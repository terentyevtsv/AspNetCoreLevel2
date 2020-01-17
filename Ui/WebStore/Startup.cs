using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebStore.Clients.Services;
using WebStore.DomainNew.Entities;
using WebStore.Interfaces;
using WebStore.Logger;
using WebStore.Services;

namespace WebStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Добавляем разрешение зависимости
            services.AddSingleton<IEmployeeService, EmployeesClient>();
            services.AddTransient<IValueService, ValuesClient>();
            services.AddScoped<IProductService, ProductsClient>();
            services.AddScoped<IOrderService, OrdersClient>();
            services.AddTransient<IUsersClient, UsersClient>();

            services.AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders();

            services.AddTransient<IUserStore<User>, CustomUserStore>();
            services.AddTransient<IUserRoleStore<User>, CustomUserStore>();
            services.AddTransient<IUserClaimStore<User>, CustomUserStore>();
            services.AddTransient<IUserPasswordStore<User>, CustomUserStore>();
            services.AddTransient<IUserTwoFactorStore<User>, CustomUserStore>();
            services.AddTransient<IUserEmailStore<User>, CustomUserStore>();
            services.AddTransient<IUserPhoneNumberStore<User>, CustomUserStore>();
            services.AddTransient<IUserLoginStore<User>, CustomUserStore>();
            services.AddTransient<IUserLockoutStore<User>, CustomUserStore>();
            services.AddTransient<IRoleStore<IdentityRole>, RolesClient>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartStore, CookieCartStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Переход при исключении
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePagesWithRedirects("~/Home/ErrorStatus/{0}");

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    "default", "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}

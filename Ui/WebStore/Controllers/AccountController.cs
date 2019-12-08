using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.ViewModels;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var loginResult = await _signInManager.PasswordSignInAsync(
                loginViewModel.UserName, loginViewModel.Password, 
                loginViewModel.RememberMe, false);

            if (!loginResult.Succeeded)
            {
                ModelState.AddModelError("", "Вход невозможен");
                return View(loginViewModel);
            }

            if (Url.IsLocalUrl(loginViewModel.ReturnUrl))
                return Redirect(loginViewModel.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View(new RegisterUserViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = registerUserViewModel.UserName,
                    Email = registerUserViewModel.Email
                };

                var createResult = await _userManager.CreateAsync(user,
                    registerUserViewModel.Password);
                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, "User");

                    return RedirectToAction("Index", "Home");
                }

                foreach (var identityError in createResult.Errors)
                {
                    ModelState.AddModelError("",
                        identityError.Description);
                }

            }

            return View(registerUserViewModel);
        }
    }
}
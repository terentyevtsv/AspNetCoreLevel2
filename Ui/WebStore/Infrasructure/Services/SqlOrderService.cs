using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.DomainNew.Entities;
using WebStore.Infrasructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Infrasructure.Services
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreContext _webStoreContext;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebStoreContext webStoreContext, 
            UserManager<User> userManager)
        {
            _webStoreContext = webStoreContext;
            _userManager = userManager;
        }

        public IEnumerable<Order> GetUserOrders(string userName)
        {
            return _webStoreContext.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .Where(o => o.User.UserName == userName)
                .ToList();
        }

        public Order GetOrderById(int id)
        {
            return _webStoreContext.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .SingleOrDefault(o => o.Id == id);
        }

        public Order CreateOrder(OrderViewModel orderViewModel, 
            CartViewModel cartViewModel, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            using (var transaction = _webStoreContext.Database.BeginTransaction())
            {
                var order = new Order
                {
                    Address = orderViewModel.Address,
                    Name = orderViewModel.Name,
                    Date = DateTime.Now,
                    Phone = orderViewModel.Phone,
                    User = user
                };

                _webStoreContext.Orders.Add(order);

                foreach (var item in cartViewModel.Items)
                {
                    var productViewModel = item.Key;

                    var product = _webStoreContext.Products
                        .SingleOrDefault(p => p.Id == productViewModel.Id);

                    if (product == null)
                    {
                        throw new InvalidOperationException(
                            "Товар не найден в БД");
                    }

                    var orderItem = new OrderItem
                    {
                        Price = product.Price,
                        Quantity = item.Value,
                        
                        Order = order,
                        Product = product
                    };

                    _webStoreContext.OrderItems.Add(orderItem);
                }

                _webStoreContext.SaveChanges();
                transaction.Commit();

                return order;
            }
        }
    }
}

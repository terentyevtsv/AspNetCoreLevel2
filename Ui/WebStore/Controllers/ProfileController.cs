using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IOrderService _orderService;

        public ProfileController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders()
        {
            var orders = _orderService
                .GetUserOrders(User.Identity.Name)
                .ToList();

            var userOrderViewModels = new 
                List<UserOrderViewModel>(orders.Count());
            foreach (var order in orders)
            {
                userOrderViewModels.Add(new UserOrderViewModel
                {
                    Id = order.Id,
                    Name = order.Name,
                    Address = order.Address,
                    Phone = order.Phone,
                    TotalSum = order.OrderItems.Sum(o => o.Price * o.Quantity)
                });
            }

            return View(userOrderViewModels);
        }
    }
}
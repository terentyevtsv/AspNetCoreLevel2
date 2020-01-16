using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Helpers;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        public IActionResult Details()
        {
            var orderDetailsViewModel = new OrderDetailsViewModel
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = new OrderViewModel()
            };
            return View(orderDetailsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var createOrderViewModel = new CreateOrderDto
                {
                    OrderViewModel = orderViewModel,
                    OrderItems = _cartService.TransformCart().Items
                        .Select(o => new OrderItem
                        {
                            Id = o.Key.Id,
                            Price = o.Key.Price,
                            Quantity = o.Value
                        })
                        .Select(o => o.ToDto()).ToList()
                };

                var orderResult = _orderService.CreateOrder(
                    createOrderViewModel, User.Identity.Name);
                _cartService.RemoveAll();

                return RedirectToAction("OrderConfirmed", 
                    new {id = orderResult.Id});
            }

            var detailsViewModel = new OrderDetailsViewModel
            {
                OrderViewModel = orderViewModel,
                CartViewModel = _cartService.TransformCart()
            };

            return View("Details", detailsViewModel);
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        public IActionResult DecrementFromCart(int id)
        {
            _cartService.DecrementFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveAll()
        {
            _cartService.RemoveAll();
            return RedirectToAction("Details");
        }

        public IActionResult AddToCart(int id)
        {
            _cartService.AddToCart(id);
            return Json(new {id, message = "Товар добавлен в корзину"});
        }

        public IActionResult GetCartView()
        {
            return ViewComponent("CartSummary");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
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
                var orderResult = _orderService.CreateOrder(orderViewModel, 
                    _cartService.TransformCart(), User.Identity.Name);
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

        public IActionResult AddToCart(int id, string returnUrl)
        {
            _cartService.AddToCart(id);
            return Redirect(returnUrl);
        }
    }
}
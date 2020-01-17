using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;

namespace WebStore.ViewComponents
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartSummaryViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cartService.TransformCart());
        }
    }
}

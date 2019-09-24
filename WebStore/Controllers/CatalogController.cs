using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Shop()
        {
            return View();
        }

        public IActionResult ProductDetails()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult BlogSingle()
        {
            return View();
        }
    }
}
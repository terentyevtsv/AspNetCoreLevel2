using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IValueService _valueService;

        public HomeController(IValueService valueService)
        {
            _valueService = valueService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _valueService.GetAsync();
            return View(values);
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult NotFound()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

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
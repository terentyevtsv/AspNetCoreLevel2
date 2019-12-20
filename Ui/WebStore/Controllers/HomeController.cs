using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Interfaces;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IValueService _valueService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IValueService valueService, ILogger<HomeController> logger)
        {
            _valueService = valueService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            throw new Exception("No pasaran!");

            _logger.LogInformation("index action requested");
            _logger.LogTrace("trace! winter is coming!");
            _logger.LogInformation("info! winter is coming!");
            _logger.LogWarning("warning! winter is coming!");
            _logger.LogDebug("debug! winter is coming!");
            _logger.LogError("error! winter is coming!");
            _logger.LogCritical("critical! winter is coming!");

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

        //home/errorstatus/403
        public IActionResult ErrorStatus(string id)
        {
            if (id == "404")
                return RedirectToAction("NotFound");

            return Content($"Статуcный код ошибки: {id}");
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult BlogSingle()
        {
            return View();
        }
    }
}
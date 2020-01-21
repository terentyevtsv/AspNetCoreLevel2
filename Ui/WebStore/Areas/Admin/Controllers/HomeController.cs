using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.Helpers;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Administrator")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList()
        {
            return View(_productService
                .GetProducts(new ProductsFilter()));
        }

        public IActionResult Edit(int? id)
        {
            var notParentCategories = _productService
                .GetCategories()
                .Where(s => s.ParentId != null);
            var brands = _productService.GetBrands();

            // выполним проверки...
            if (id is null)
                return View(new ProductViewModel
                {
                    Categories = new SelectList(notParentCategories, "Id", "Name"),
                    Brands = new SelectList(brands, "Id", "Name")
                });

            var product = _productService.GetProductById(id.Value);
            if (product is null)
                return NotFound();

            return View(product.ToViewModel(notParentCategories, brands));
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            var notParentCategories = _productService
                .GetCategories()
                .Where(s => s.ParentId != null);
            var brands = _productService.GetBrands();

            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    _productService.UpdateProduct(model.ToDto());
                }
                else
                {
                    _productService.CreateProduct(model.ToDto());
                }

                return RedirectToAction(nameof(ProductList));
            }

            model.Brands = new SelectList(brands, "Id", "Name", model.BrandId);
            model.Categories = new SelectList(notParentCategories, "Id", "Name", model.CategoryId);

            return View(model);

        }
        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound();

            return View(product.ToViewModel(new List<Category>(), new List<Brand>()));
        }

        [HttpPost]
        public IActionResult Delete(ProductViewModel model)
        {
            _productService.DeleteProduct(model.Id);
            return RedirectToAction(nameof(ProductList));
        }

    }
}
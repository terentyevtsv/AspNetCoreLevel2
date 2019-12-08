using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.Filters;
using WebStore.Infrasructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;

        public CatalogController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Shop(int? categoryId, int? brandId)
        {
            var products = _productService.GetProducts(
                new ProductsFilter
                {
                    CategoryId = categoryId,
                    BrandId = brandId
                });

            var catalogViewModel = new CatalogViewModel
            {
                BrandId = brandId,
                CategoryId = categoryId,
                Products = products.Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        ImageUrl = p.ImageUrl,
                        Name = p.Name,
                        Order = p.Order,
                        Price = p.Price,
                        IsSale = p.IsSale,
                        IsNew = p.IsNew,
                        BrandName = p.Brand?.Name ?? string.Empty
                    })
                    .OrderBy(p => p.Order)
                    .ToList()
            };

            return View(catalogViewModel);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return RedirectToAction("NotFound", "Home");

            var productViewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Order = product.Order,
                IsNew = product.IsNew,
                IsSale = product.IsSale,
                Price = product.Price,
                BrandName = product.Brand?.Name ?? string.Empty
            };

            return View(productViewModel);
        }
    }
}
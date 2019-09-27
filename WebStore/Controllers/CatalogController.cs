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
                        IsNew = p.IsNew
                    })
                    .OrderBy(p => p.Order)
                    .ToList()
            };

            return View(catalogViewModel);
        }

        public IActionResult ProductDetails()
        {
            return View();
        }
    }
}
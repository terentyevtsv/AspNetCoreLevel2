using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;

        public CatalogController(IProductService productService,
            IConfiguration configuration)
        {
            _productService = productService;
            _configuration = configuration;
        }

        public IActionResult Shop(int? categoryId, int? brandId, int page = 1)
        {
            var pageSize = int.Parse(_configuration["PageSize"]);

            var products = _productService.GetProducts(
                new ProductsFilter
                {
                    CategoryId = categoryId,
                    BrandId = brandId,
                    PageSize = pageSize,
                    Page = page
                });

            var catalogViewModel = new CatalogViewModel
            {
                BrandId = brandId,
                CategoryId = categoryId,
                Products = products.Products.Select(p => new ProductViewModel
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
                    .ToList(),
                PageViewModel = new PageViewModel
                {
                    PageSize = pageSize,
                    PageNumber = page,
                    TotalItems = products.TotalCount
                }
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
using System;
using System.Collections.Generic;
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


            var products = GetProducts(categoryId, brandId, page, out var totalCount)
                .ToList();

            var catalogViewModel = new CatalogViewModel
            {
                BrandId = brandId,
                CategoryId = categoryId,
                Products = products,
                PageViewModel = new PageViewModel
                {
                    PageSize = pageSize,
                    PageNumber = page,
                    TotalItems = totalCount
                }
            };

            return View(catalogViewModel);
        }

        public IActionResult GetFilteredItems(int? categoryId, int? brandId, int page = 1)
        {
            var productsModel = GetProducts(categoryId, brandId, page, out var totalCount);

            return PartialView("Partial/_ProductItems", productsModel);
        }

        private IEnumerable<ProductViewModel> GetProducts(int? categoryId, int? brandId, int page, out int totalCount)
        {
            var products = _productService.GetProducts(new ProductsFilter
            {
                CategoryId = categoryId,
                BrandId = brandId,
                Page = page,
                PageSize = int.Parse(_configuration["PageSize"])
            });
            totalCount = products.TotalCount;

            return products.Products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    BrandName = p.Brand?.Name ?? String.Empty,
                    IsNew = p.IsNew,
                    IsSale = p.IsSale
                })
                .OrderBy(p => p.Order)
                .ToList();
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
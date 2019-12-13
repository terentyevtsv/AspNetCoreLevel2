using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;
using WebStore.Interfaces;

namespace WebStore.ServicesHosting.Controllers
{
    [Route("api/products")]
    [Produces("application/json")]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductService
    {
        private readonly IProductService _productService;

        public ProductsApiController(IProductService productService)
        {
            _productService = productService ?? 
                              throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet("categories")]
        public IEnumerable<Category> GetCategories()
        {
            var categories = _productService.GetCategories();
            return categories;
        }

        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands()
        {
            var brands = _productService.GetBrands();
            return brands;
        }

        [HttpPost, ActionName("Post")]
        public IEnumerable<ProductDto> GetProducts([FromBody]ProductsFilter filter)
        {
            var products = _productService.GetProducts(filter);
            return products;
        }

        [HttpGet("{id}"), ActionName("Get")]
        public ProductDto GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            return product;
        }
    }
}
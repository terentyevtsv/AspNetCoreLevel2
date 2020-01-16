using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using WebStore.Interfaces;

namespace WebStore.Controllers
{
    public class SitemapController : Controller
    {
        private readonly IProductService _productService;

        public SitemapController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            // список ссылок основного меню
            var nodes = new List<SitemapNode>
            {
                new SitemapNode(Url.Action("Index", "Home")),
                new SitemapNode(Url.Action("Shop", "Catalog")),
                new SitemapNode(Url.Action("BlogSingle","Home")),
                new SitemapNode(Url.Action("Blog","Home")),
                new SitemapNode(Url.Action("ContactUs","Home"))
            };

            // список всех категорий товаров
            var categories = _productService.GetCategories();
            foreach (var category in categories)
            {
                if (category.ParentId.HasValue)
                    nodes.Add(new SitemapNode(Url.Action(
                        "Shop",
                        "Catalog",
                        new { categoryId = category.Id })));
            }

            // список всех брендов
            var brands = _productService.GetBrands();
            foreach (var brand in brands)
            {
                nodes.Add(new SitemapNode(Url.Action(
                    "Shop",
                    "Catalog",
                    new { brandId = brand.Id })));
            }

            // список всех страниц товаров
            var products = _productService.GetProducts(new DomainNew.Filters.ProductsFilter());
            foreach (var productDto in products.Products)
            {
                nodes.Add(new SitemapNode(Url.Action(
                    "ProductDetails",
                    "Catalog",
                    new { id = productDto.Id })));
            }

            return new SitemapProvider()
                .CreateSitemap(new SitemapModel(nodes));
        }
    }
}
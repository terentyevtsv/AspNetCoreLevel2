using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;
using WebStore.Interfaces;

namespace WebStore.Clients.Services
{
    public class ProductsClient : BaseClient, IProductService
    {
        public ProductsClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/products";
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = Get<List<Category>>($"{ServiceAddress}/categories");
            return categories;
        }

        public IEnumerable<Brand> GetBrands()
        {
            var brands = Get<List<Brand>>($"{ServiceAddress}/brands");
            return brands;
        }

        public IEnumerable<ProductDto> GetProducts(ProductsFilter filter)
        {
            var response = Post($"{ServiceAddress}", filter);
            var products = response.Content.ReadAsAsync<List<ProductDto>>().Result;
            return products;
        }

        public ProductDto GetProductById(int id)
        {
            var product = Get<ProductDto>($"{ServiceAddress}/{id}");
            return product;
        }

        public Category GetCategoryById(int id)
        {
            var url = $"{ServiceAddress}/categories/{id}";
            var category = Get<Category>(url);
            return category;
        }

        public Brand GetBrandById(int id)
        {
            var url = $"{ServiceAddress}/brands/{id}";
            var brand = Get<Brand>(url);
            return brand;
        }
    }
}

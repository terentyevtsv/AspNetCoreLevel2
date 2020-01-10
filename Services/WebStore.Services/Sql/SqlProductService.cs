using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.Helpers;
using WebStore.Interfaces;

namespace WebStore.Services.Sql
{
    public class SqlProductService : IProductService
    {
        private readonly WebStoreContext _webStoreContext;

        public SqlProductService(
            WebStoreContext webStoreContext)
        {
            _webStoreContext = webStoreContext;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _webStoreContext
                .Categories.ToList();
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _webStoreContext
                .Brands.ToList();
        }

        public IEnumerable<ProductDto> GetProducts(ProductsFilter filter)
        {
            var query = _webStoreContext
                .Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .AsQueryable();

            if (filter.BrandId.HasValue)
            {
                query = query.Where(c => c.BrandId.HasValue && 
                                         c.BrandId.Value.Equals(
                                             filter.BrandId.Value));
            }

            if (filter.CategoryId.HasValue)
            {
                query = query.Where(c => c.CategoryId
                    .Equals(filter.CategoryId.Value));
            }

            return query.Select(p => p.ToDto()).ToList();
        }

        public ProductDto GetProductById(int id)
        {
            var product = _webStoreContext
                .Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .SingleOrDefault(p => p.Id == id);

            if (product != null)
                return product.ToDto();

            return null;
        }

        public Category GetCategoryById(int id)
        {
            var category = _webStoreContext.Categories
                .SingleOrDefault(c => c.Id == id);
            return category;
        }

        public Brand GetBrandById(int id)
        {
            var brand = _webStoreContext.Brands
                .SingleOrDefault(b => b.Id == id);
            return brand;
        }
    }
}

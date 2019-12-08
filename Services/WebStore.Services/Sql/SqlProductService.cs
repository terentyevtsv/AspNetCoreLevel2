using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;
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

        public IEnumerable<Product> GetProducts(ProductsFilter filter)
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

            return query.ToList();
        }

        public Product GetProductById(int id)
        {
            var product = _webStoreContext
                .Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .SingleOrDefault(p => p.Id == id);
            return product;
        }
    }
}

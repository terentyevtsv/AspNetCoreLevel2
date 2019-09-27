using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;
using WebStore.Infrasructure.Interfaces;

namespace WebStore.Infrasructure.Services
{
    public class DbProductService : IProductService
    {
        private readonly WebStoreContext _webStoreContext;

        public DbProductService(
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
                .Products.AsQueryable();
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
    }
}

using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Domain.Filters;

namespace WebStore.Infrasructure.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Все секции
        /// </summary>
        /// <returns></returns>
        IEnumerable<Category> GetCategories();

        /// <summary>
        /// Все бренды
        /// </summary>
        /// <returns></returns>
        IEnumerable<Brand> GetBrands();

        /// <summary>
        /// Товары
        /// </summary>
        /// <returns></returns>
        IEnumerable<Product> GetProducts(ProductsFilter filter);
    }
}

using System.Collections.Generic;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;

namespace WebStore.Interfaces
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

        /// <summary>
        /// Получить товар по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetProductById(int id);
    }
}

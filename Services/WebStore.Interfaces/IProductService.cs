using System.Collections.Generic;
using WebStore.DomainNew.Dto;
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
        PagedProductDto GetProducts(ProductsFilter filter);

        /// <summary>
        /// Получить товар по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProductDto GetProductById(int id);

        /// <summary>
        /// Категория по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Category GetCategoryById(int id);

        /// <summary>
        /// Бренд по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Brand GetBrandById(int id);

        /// <summary>Создать товар</summary>
        /// <param name="product">Сущность Product</param>
        SaveResultDto CreateProduct(ProductDto product);

        /// <summary>Обновить товар</summary>
        /// <param name="product">Сущность Product</param>
        SaveResultDto UpdateProduct(ProductDto product);

        /// <summary>Удалить товар</summary>
        /// <param name="productId">Id товара</param>
        SaveResultDto DeleteProduct(int productId);
    }
}

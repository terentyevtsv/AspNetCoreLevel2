using System;
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

        public PagedProductDto GetProducts(ProductsFilter filter)
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

            var model = new PagedProductDto { TotalCount = query.Count() };

            if (filter.PageSize.HasValue)
            {
                model.Products = query
                    .Skip((filter.Page - 1) * (int)filter.PageSize)
                    .Take((int)filter.PageSize)
                    .Select(p => p.ToDto())
                    .ToList();
            }
            else
            {
                model.Products = query
                    .Select(p => p.ToDto())
                    .ToList();
            }

            return model;
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

        public SaveResultDto CreateProduct(ProductDto product)
        {
            try
            {
                _webStoreContext.Products.Add(product.ToProduct());
                _webStoreContext.SaveChanges();

                return new SaveResultDto(true);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new SaveResultDto(false, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return new SaveResultDto(false, ex.Message);
            }
            catch (Exception ex)
            {
                return new SaveResultDto(false, ex.Message);
            }
        }

        public SaveResultDto UpdateProduct(ProductDto productDto)
        {
            var product = _webStoreContext.Products.FirstOrDefault(p => p.Id == productDto.Id);
            if (product == null)
            {
                return new SaveResultDto(false, "Entity does not exist");
            }

            // скопируем все поля модели
            product.BrandId = productDto.Brand.Id;
            product.CategoryId = productDto.Category.Id;
            product.ImageUrl = productDto.ImageUrl;
            product.Order = productDto.Order;
            product.Price = productDto.Price;
            product.Name = productDto.Name;

            try
            {
                _webStoreContext.SaveChanges();
                return new SaveResultDto(true);
            }
            catch (Exception ex)
            {
                return new SaveResultDto(false, ex.Message);
            }
        }

        public SaveResultDto DeleteProduct(int productId)
        {
            var product = _webStoreContext.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return new SaveResultDto(false, "Entity does not exist");
            }

            try
            {
                _webStoreContext.Remove(product);
                _webStoreContext.SaveChanges();

                return new SaveResultDto(true);
            }
            catch (Exception ex)
            {
                return new SaveResultDto(false, ex.Message);
            }
        }
    }
}

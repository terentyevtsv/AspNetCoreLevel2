using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.ViewModels;

namespace WebStore.DomainNew.Helpers
{
    public static class Mapper
    {
        public static ProductDto ToDto(this Product product)
        {
            var productDto = new ProductDto
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                IsNew = product.IsNew,
                IsSale = product.IsSale,
                // Непонятно почему здесь
                Brand = product.BrandId.HasValue
                    ? new BrandDto
                    {
                        Id = product.BrandId.Value,
                        Name = product.Brand.Name,
                        Order = product.Brand.Order
                    }
                    : null,
                Category = new CategoryDto
                {
                    Id = product.CategoryId,
                    Name = product.Category.Name
                }
            };

            return productDto;
        }

        public static OrderDto ToDto(this Order order)
        {
            var orderDto = new OrderDto
            {
                Id = order.Id,
                Name = order.Name,
                Phone = order.Phone,
                Address = order.Address,
                Date = order.Date,
                // и здесь инициализируем поля связанных таблиц Entity
                OrderItems = order.OrderItems
                    .Select(o => o.ToDto())
                    .ToList()
            };

            return orderDto;
        }

        // а здесь инициализируются только значимые поля, хотя OrderItem содержит еще
        // свойства Order и Product. 

        public static OrderItemDto ToDto(this OrderItem orderItem)
        {
            var orderItemDto = new OrderItemDto
            {
                Id = orderItem.Id,
                Price = orderItem.Price,
                Quantity = orderItem.Quantity
            };

            return orderItemDto;
        }

        public static ProductDto ToDto(this ProductViewModel p) =>
            new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                // добавим информацию о бренде, если она есть
                Brand = p.BrandId.HasValue ? new BrandDto { Id = p.BrandId.Value, Name = p.BrandName } : null,
                Category = new CategoryDto
                {
                    Id = p.CategoryId,
                    Name = p.Category
                }
            };

        public static Product ToProduct(this ProductDto productDto) =>
            new Product
            {
                BrandId = productDto.Brand?.Id,
                CategoryId = productDto.Category.Id,
                Name = productDto.Name,
                ImageUrl = productDto.ImageUrl,
                Order = productDto.Order,
                Price = productDto.Price
            };

        public static ProductViewModel ToViewModel(this ProductDto product, 
            IEnumerable<Category> categories, IEnumerable<Brand> brands) =>
            new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = product.Category.Name,
                CategoryId = product.Category.Id,
                BrandName = product.Brand?.Name,
                BrandId = product.Brand?.Id,
                Brands = new SelectList(brands, "Id", "Name", product.Brand?.Id),
                Categories = new SelectList(categories, "Id", "Name", product.Category.Id)
            };
    }
}

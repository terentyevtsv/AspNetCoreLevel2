using System.Linq;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Entities;

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
    }
}

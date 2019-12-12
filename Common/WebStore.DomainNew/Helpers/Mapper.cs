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
                Brand = product.BrandId.HasValue
                    ? new BrandDto
                    {
                        Id = product.BrandId.Value,
                        Name = product.Brand.Name,
                        Order = product.Brand.Order
                    }
                    : null
            };

            return productDto;
        }
    }
}

using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int CategoryId { get; set; }

        public int? BrandId { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// адрес картинки
        /// </summary>
        public string ImageUrl { get; set; }
    }
}

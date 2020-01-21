using System.ComponentModel.DataAnnotations.Schema;
using WebStore.DomainNew.Entities.Base;
using WebStore.DomainNew.Entities.Base.Interfaces;

namespace WebStore.DomainNew.Entities
{
    [Table("Products")]
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int? BrandId { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// адрес картинки
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Новый товар
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Распродажа
        /// </summary>
        public bool IsSale { get; set; }

        /// <summary>Удален товар или нет</summary>
        public bool IsDeleted { get; set; }
    }
}

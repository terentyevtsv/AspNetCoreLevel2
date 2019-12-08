using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.DomainNew.Entities.Base;
using WebStore.DomainNew.Entities.Base.Interfaces;

namespace WebStore.DomainNew.Entities
{
    /// <summary>
    /// Категория товара
    /// </summary>
    [Table("Categories")]
    public class Category : NamedEntity, IOrderedEntity
    {
        /// <summary>
        /// Родительская секция
        /// </summary>
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Category ParentCategory { get; set; }

        public int Order { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

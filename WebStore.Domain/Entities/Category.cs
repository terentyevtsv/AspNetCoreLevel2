using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    /// <summary>
    /// Категория товара
    /// </summary>
    public class Category : NamedEntity, IOrderedEntity
    {
        /// <summary>
        /// Родительская секция
        /// </summary>
        public int? ParentId { get; set; }

        public int Order { get; set; }
    }
}

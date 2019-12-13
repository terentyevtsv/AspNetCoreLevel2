using WebStore.DomainNew.Entities.Base.Interfaces;

namespace WebStore.DomainNew.Dto
{
    public class OrderItemDto : IBaseEntity
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}

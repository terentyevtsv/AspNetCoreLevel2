using System.Collections.Generic;
using WebStore.DomainNew.ViewModels;

namespace WebStore.DomainNew.Dto
{
    public class CreateOrderDto
    {
        public OrderViewModel OrderViewModel { get; set; }

        public IList<OrderItemDto> OrderItems { get; set; }
    }
}

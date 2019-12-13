using System.Collections.Generic;
using WebStore.DomainNew.Dto;

namespace WebStore.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetUserOrders(string userName);

        OrderDto GetOrderById(int id);

        OrderDto CreateOrder(CreateOrderDto createOrderDto, 
            string userName);
    }
}

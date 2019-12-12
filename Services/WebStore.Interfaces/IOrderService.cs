using System.Collections.Generic;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.ViewModels;

namespace WebStore.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetUserOrders(string userName);

        OrderDto GetOrderById(int id);

        OrderDto CreateOrder(CreateOrderViewModel createOrderViewModel, 
            string userName);
    }
}

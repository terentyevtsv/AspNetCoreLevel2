using System.Collections.Generic;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.ViewModels;

namespace WebStore.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetUserOrders(string userName);

        Order GetOrderById(int id);

        Order CreateOrder(OrderViewModel orderViewModel, 
            CartViewModel cartViewModel, string userName);
    }
}

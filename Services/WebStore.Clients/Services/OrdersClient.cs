using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.Clients.Services
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/orders";
        }

        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            var orders = Get<List<OrderDto>>($"{ServiceAddress}/user/{userName}");
            return orders;
        }

        public OrderDto GetOrderById(int id)
        {
            var order = Get<OrderDto>($"{ServiceAddress}/{id}");
            return order;
        }

        public OrderDto CreateOrder(CreateOrderDto createOrderDto, 
            string userName)
        {
            var response = Post($"{ServiceAddress}/{userName}", createOrderDto);

            return response.Content.ReadAsAsync<OrderDto>().Result;
        }
    }
}

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.ServicesHosting.Controllers
{
    [Route("api/orders")]
    [Produces("application/json")]
    [ApiController]
    public class OrderApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _orderService;

        public OrderApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("user/{userName}")]
        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            var orders = _orderService.GetUserOrders(userName);
            return orders;
        }

        [HttpGet("{id}")]
        public OrderDto GetOrderById(int id)
        {
            var order = _orderService.GetOrderById(id);
            return order;
        }

        [HttpPost("{userName?}")]
        public OrderDto CreateOrder([FromBody] CreateOrderDto createOrderDto, 
            string userName)
        {
            var order = _orderService.CreateOrder(
                createOrderDto, userName);
            return order;
        }
    }
}
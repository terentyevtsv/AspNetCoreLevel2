using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebStore.Controllers;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;
using Xunit;

namespace WebStore.Tests
{
    public class CartControllerTests
    {
        private readonly CartController _cartController;
        private readonly Mock<ICartService> _mockCartService;
        private readonly Mock<IOrderService> _mockOrderService;

        public CartControllerTests()
        {
            _mockCartService = new Mock<ICartService>();
            _mockOrderService = new Mock<IOrderService>();

            _cartController = new CartController(
                _mockCartService.Object, _mockOrderService.Object);
        }

        [Fact]
        public void CheckoutModelStateInvalidModelStateReturnsViewModel()
        {
            _cartController.ModelState
                .AddModelError("error", "Invalid Model");

            var result = _cartController.Checkout(new OrderViewModel
            {
                Name = "test"
            });

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OrderDetailsViewModel>(
                viewResult.Model);
            Assert.Equal("test", 
                model.OrderViewModel.Name);
        }

        [Fact]
        public void CheckoutCallsServiceAndReturnRedirect()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "1")
                }));
            _mockCartService
                .Setup(c => c.TransformCart())
                .Returns(new CartViewModel
                {
                    Items = new Dictionary<ProductViewModel, int>
                    {
                        {new ProductViewModel(), 1}
                    }
                });

            _mockOrderService
                .Setup(o =>
                    o.CreateOrder(It.IsAny<CreateOrderDto>(),
                        It.IsAny<string>()))
                .Returns(new OrderDto
                {
                    Id = 1
                });

            _cartController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            var result = _cartController.Checkout(new OrderViewModel
            {
                Name = "test",
                Address = "",
                Phone = ""
            });

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectResult.ControllerName);
            Assert.Equal("OrderConfirmed", 
                redirectResult.ActionName);
            Assert.Equal(1, 
                redirectResult.RouteValues["id"]);
        }
    }
}

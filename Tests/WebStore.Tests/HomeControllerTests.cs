using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebStore.Controllers;
using WebStore.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace WebStore.Tests
{
    public class HomeControllerTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HomeController _controller;

        public HomeControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var mockService = new Mock<IValueService>();
            mockService.Setup(s => s.GetAsync())
                .ReturnsAsync(new List<string>
                {
                    "1", "2"
                });
            _controller = new HomeController(
                mockService.Object, null);
        }

        [Theory(DisplayName = "Add Numbers")]
        [InlineData(4, 5, 9)]
        [InlineData(2, 3, 5)]
        public void TestAddNumbers(int x, int y, int expectedResult)
        {
            _testOutputHelper.WriteLine($"current x={x}");

            Assert.Equal(4, x);
            Assert.Equal(5, y);
        }

        [Fact]
        public async Task IndexMethodReturnsViewWithValues()
        {
            _testOutputHelper.WriteLine("This is extra output...");
            // Arange - заготовка входных данных в конструкторе

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(
                viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void ContactUsReturnsView()
        {
            // Act
            var result = _controller.ContactUs();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ErrorStatus404RedirectsToNotFound()
        {
            // Act
            var result = _controller.ErrorStatus("404");

            // Assert
            var redirectToActionResult = Assert
                .IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("NotFound", 
                redirectToActionResult.ActionName);
        }

        [Fact]
        public void ErrorStatusAnotherReturnsContentResult()
        {
            var result = _controller.ErrorStatus("405");

            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal("Статуcный код ошибки: 405", 
                contentResult.Content);
        }

        [Fact]
        public void CheckoutReturnsView()
        {
            var result = _controller.Checkout();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void BlogSingleReturnsView()
        {
            var result = _controller.BlogSingle();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void BlogReturnsView()
        {
            var result = _controller.Blog();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ErrorReturnsView()
        {
            var result = _controller.Error();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void NotFoundReturnsView()
        {
            var result = _controller.NotFound();
            Assert.IsType<ViewResult>(result);
        }
    }
}

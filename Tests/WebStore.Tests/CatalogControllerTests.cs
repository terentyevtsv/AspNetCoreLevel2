using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using WebStore.Controllers;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;
using Xunit;

namespace WebStore.Tests
{
    public class CatalogControllerTests
    {
        private readonly CatalogController _catalogController;
        private readonly Mock<IProductService> _productServiceMock;

        public CatalogControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            var mockConfiguration = new Mock<IConfiguration>();

            _catalogController = new CatalogController(
                _productServiceMock.Object, mockConfiguration.Object);
        }

        // проверим, что метод controller.ProductDetails() возвращает корректную модель
        [Fact]
        public void ProductDetails_Returns_View_With_Correct_Item()
        {
            // Arrange
            // заглушка для нужного метода
            _productServiceMock
                .Setup(p =>
                    p.GetProductById(It.IsAny<int>()))
                .Returns(new ProductDto
                {
                    Id = 1,
                    Name = "Test",
                    ImageUrl = "TestImage.jpg",
                    Order = 0,
                    Price = 10,
                    Brand = new BrandDto
                    {
                        Id = 1,
                        Name = "TestBrand"
                    }
                });

            // Act
            // вызываем тестируемый метод
            var result = _catalogController.ProductDetails(1);
            
            // Assert
            // проверим тип пришедших данных
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(viewResult.Model);

            // проверим поля модели
            Assert.Equal(1, model.Id);
            Assert.Equal("Test", model.Name);
            Assert.Equal(10, model.Price);
            Assert.Equal("TestBrand", model.BrandName);
        }

        // метод controller.ProductDetails() должен вернуть 404 NotFound, если не найден товар по id
        [Fact]
        public void ProductDetails_Returns_NotFound()
        {
            // Arrange
            // заглушка для метода 
            // этот метод всегда будет возвращать null
            _productServiceMock.Setup(p => p.GetProductById(
                    It.IsAny<int>()))
                .Returns(() => null);

            // Act
            var result = _catalogController.ProductDetails(1);

            // Assert
            // ответ от контроллера должен быть типа RedirectToActionResult 
            // контроллера Home действия NotFound
            var redirectToActionResult = Assert
                .IsType<RedirectToActionResult>(result);
            Assert.Equal("Home",
                redirectToActionResult.ControllerName);
            Assert.Equal("NotFound", 
                redirectToActionResult.ActionName);
        }

        // проверяем корректную работу метода controller.Shop()
        [Fact]
        public void ShopMethodReturnsCorrectView()
        {
            // Arrange
            // заглушка для метода 
            // должна возвращать какой-то список товаров List<ProductDto>
            var pagedProductDto = new PagedProductDto
            {
                Products = new List<ProductDto>
                {
                    new ProductDto
                    {
                        Id = 1,
                        Name = "Test",
                        ImageUrl = "TestImage.jpg",
                        Order = 0,
                        Price = 10,
                        Brand = new BrandDto
                        {
                            Id = 1,
                            Name = "TestBrand"
                        }
                    },

                    new ProductDto
                    {
                        Id = 2,
                        Name = "Test2",
                        ImageUrl = "TestImage2.jpg",
                        Order = 1,
                        Price = 22,
                        Brand = new BrandDto
                        {
                            Id = 1,
                            Name = "TestBrand"
                        }
                    }
                }
            };

            _productServiceMock
                .Setup(p =>
                    p.GetProducts(It.IsAny<ProductsFilter>()))
                .Returns(pagedProductDto);

            // Act
            // вызываем тестируемый метод
            var result = _catalogController.Shop(1, 5);

            // Assert
            // проверяем тип данных
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CatalogViewModel>(
                viewResult.Model);

            // проверяем поля модели
            Assert.Equal(1, model.CategoryId);
            Assert.Equal(5, model.BrandId);
            Assert.Equal(2, model.Products.Count());

            Assert.Equal(1,
                model.Products.ToList()[0].Id);
            Assert.Equal("TestImage.jpg",
                model.Products.ToList()[0].ImageUrl);
            Assert.Equal(10,
                model.Products.ToList()[0].Price);

            Assert.Equal(2,
                model.Products.ToList()[1].Id);
            Assert.Equal("TestImage2.jpg",
                model.Products.ToList()[1].ImageUrl);
            Assert.Equal(22,
                model.Products.ToList()[1].Price);
        }
    }
}

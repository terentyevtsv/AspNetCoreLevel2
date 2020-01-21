using System.Collections.Generic;
using System.Linq;
using Moq;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;
using WebStore.Services;
using Xunit;

namespace WebStore.Tests
{
    public class CartServiceTests
    {
        [Fact]
        public void CartClassItemsCountReturnsCorrectQuantity()
        {
            var cart = new Cart
            {
                CartItems = new List<CartItem>
                {
                    new CartItem
                    {
                        ProductId = 1,
                        Quantity = 10
                    },
                    new CartItem
                    {
                        ProductId = 2,
                        Quantity = 5
                    }
                }
            };

            Assert.Equal(15, cart.ItemsCount);
        }

        [Fact]
        public void CartViewModelReturnsCorrectItemsCount()
        {
            var cartViewModel = new CartViewModel
            {
                Items = new Dictionary<ProductViewModel, int>
                {
                    {
                        new ProductViewModel
                        {
                            Id = 1,
                            Name = "TestItem",
                            Price = 5.0m
                        }, 10

                    },
                    {
                        new ProductViewModel
                        {
                            Id = 2,
                            Name = "TestItem",
                            Price = 1.0m
                        }, 20
                    }
                }
            };

            Assert.Equal(30, 
                cartViewModel.ItemsCount);
        }

        [Fact]
        public void CartServiceAddToCartWorksCorrect()
        {
            // Arrange
            // подготовим пустую корзину
            var cart = new Cart
            {
                CartItems =  new List<CartItem>()
            };

            var productData = new Mock<IProductService>();
            var cartStore = new Mock<ICartStore>();
            cartStore
                .Setup(c => c.Cart)
                .Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            // Act
            cartService.AddToCart(5);

            // Assert
            Assert.Equal(1, cart.ItemsCount);
            Assert.Equal(1, cart.CartItems.Count);
            Assert.Equal(5, cart.CartItems[0].ProductId);
        }

        [Fact]
        public void CartServiceAddToCartIncrementQuantity()
        {
            // Arrange
            // подготовим корзину с товарами
            var cart = new Cart
            {
                CartItems = new List<CartItem>
                {
                    new CartItem
                    {
                        ProductId = 5,
                        Quantity = 2
                    }
                }
            };

            var productData = new Mock<IProductService>();
            var cartStore = new Mock<ICartStore>();
            cartStore
                .Setup(c => c.Cart)
                .Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            // Act
            cartService.AddToCart(5);

            // Assert
            Assert.Equal(1, cart.CartItems.Count);
            Assert.Equal(3, cart.ItemsCount);
        }

        [Fact]
        public void CartServiceRemoveFromCartRemovesCorrectItem()
        {
            // Arrange
            // корзина с товарами
            var cart = new Cart
            {
                CartItems = new List<CartItem>
                {
                    new CartItem
                    {
                        ProductId = 1,
                        Quantity = 3
                    },
                    new CartItem
                    {
                        ProductId = 2,
                        Quantity = 1
                    }
                }
            };

            var productData = new Mock<IProductService>();
            var cartStore = new Mock<ICartStore>();
            cartStore
                .Setup(c => c.Cart)
                .Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            // Act
            // удаляем товар
            cartService.RemoveFromCart(1);

            // Assert
            // должен остаться 1 товар в корзине
            Assert.Equal(1, cart.CartItems.Count);
            Assert.Equal(2, cart.CartItems[0].ProductId);
        }

        [Fact]
        public void CartServiceRemoveAllClearCart()
        {
            // Arrange
            // корзина с товарами
            var cart = new Cart
            {
                CartItems = new List<CartItem>
                {
                    new CartItem
                    {
                        ProductId = 1,
                        Quantity = 3
                    },
                    new CartItem
                    {
                        ProductId = 2, 
                        Quantity = 1
                    }
                }
            };

            var productData = new Mock<IProductService>();
            var cartStore = new Mock<ICartStore>();
            cartStore
                .Setup(c => c.Cart)
                .Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            // Act
            // удаляем все
            cartService.RemoveAll();

            // Assert
            // должна быть пустая корзина
            Assert.Equal(0, cart.CartItems.Count);
        }

        [Fact]
        public void CartServiceRemoveItemWhenDecrement()
        {
            // Arrange
            // такая же корзина, как и в прежнем тесте
            var cart = new Cart
            {
                CartItems = new List<CartItem>
                {
                    new CartItem
                    {
                        ProductId = 1,
                        Quantity = 3
                    },
                    new CartItem
                    {
                        ProductId = 2,
                        Quantity = 1
                    }
                }
            };

            var productData = new Mock<IProductService>();
            var cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            // Act
            cartService.DecrementFromCart(2);

            // Assert
            // осталось 3 из 4 товаров
            Assert.Equal(3, cart.ItemsCount);
            // осталось только 1 наименование товара
            Assert.Equal(1, cart.CartItems.Count);
        }

        [Fact]
        public void CartServiceTransformCartWorksCorrect()
        {
            // Arrange
            var cart = new Cart
            {
                CartItems = new List<CartItem>
                {
                    new CartItem
                    {
                        ProductId = 1,
                        Quantity = 4
                    }
                }
            };
            var pagedProductDto = new PagedProductDto
            {
                Products = new List<ProductDto>
                { 
                    new ProductDto
                    {
                        Id = 1,
                        ImageUrl = "",
                        Name = "Test",
                        Order = 0,
                        Price = 1.11m
                    }
                }
            };

            var productData = new Mock<IProductService>();
            productData
                .Setup(c => 
                    c.GetProducts(It.IsAny<ProductsFilter>()))
                .Returns(pagedProductDto);
            var cartStore = new Mock<ICartStore>();
            cartStore
                .Setup(c => c.Cart)
                .Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            // Act
            var result = cartService.TransformCart();

            // Assert
            Assert.Equal(4, result.ItemsCount);
            Assert.Equal(1.11m, result.Items.First().Key.Price);
        }

        [Fact]
        public void CartService_Decrement_Correct()
        {
            var cart = new Cart
            {
                CartItems = new List<CartItem>
                {
                    new CartItem {ProductId = 1,Quantity = 3},
                    new CartItem {ProductId = 2, Quantity = 1}
                }
            };

            var productData = new Mock<IProductService>();
            var cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            var cartService = new CartService(
                productData.Object,
                cartStore.Object);

            cartService.DecrementFromCart(1);

            Assert.Equal(3, cart.ItemsCount);
            Assert.Equal(2, cart.CartItems.Count);
            Assert.Equal(1, cart.CartItems[0].ProductId);
        }
    }
}

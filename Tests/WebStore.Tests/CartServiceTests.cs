using System.Collections.Generic;
using WebStore.DomainNew.ViewModels;
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
    }
}

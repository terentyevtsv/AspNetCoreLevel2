using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebStore.DomainNew.Filters;
using WebStore.Infrasructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Infrasructure.Services
{
    public class CookieCartService : ICartService
    {
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        private Cart Cart
        {
            get
            {
                string cookie = _httpContextAccessor
                    .HttpContext
                    .Request
                    .Cookies[_cartName];

                string json;
                Cart cart;

                if (cookie == null)
                {
                    cart = new Cart
                    {
                        CartItems = new List<CartItem>()
                    };
                    json = JsonConvert.SerializeObject(cart);

                    _httpContextAccessor
                        .HttpContext
                        .Response
                        .Cookies
                        .Append(_cartName, json, 
                            new CookieOptions
                            {
                                Expires = DateTime.Now.AddDays(1)
                            });

                    return cart;
                }

                json = cookie;
                cart = JsonConvert.DeserializeObject<Cart>(json);

                _httpContextAccessor
                    .HttpContext
                    .Response
                    .Cookies
                    .Delete(_cartName);

                _httpContextAccessor
                    .HttpContext
                    .Response
                    .Cookies
                    .Append(
                        _cartName, json, new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(1)
                        });

                return cart;
            }

            set
            {
                var json = JsonConvert.SerializeObject(value);

                _httpContextAccessor
                    .HttpContext
                    .Response
                    .Cookies
                    .Delete(_cartName);
                _httpContextAccessor
                    .HttpContext
                    .Response
                    .Cookies
                    .Append(_cartName, json, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });
            }
        }

        public CookieCartService(IProductService productService,
            IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
            var cartName = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                ? _httpContextAccessor.HttpContext.User.Identity.Name
                : "";
            _cartName = $"cart{cartName}";
        }

        public void DecrementFromCart(int id)
        {
            var cart = Cart;
            var item = cart.CartItems
                .SingleOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                if (item.Quantity > 0)
                    item.Quantity--;

                if (item.Quantity == 0)
                    cart.CartItems.Remove(item);
            }

            Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = Cart;
            var item = cart.CartItems
                .SingleOrDefault(c => c.ProductId == id);
            if (item != null)
                cart.CartItems.Remove(item);

            Cart = cart;
        }

        public void RemoveAll()
        {
            Cart = new Cart
            {
                CartItems = new List<CartItem>()
            };
        }

        public void AddToCart(int id)
        {
            var cart = Cart;

            var item = cart.CartItems
                .SingleOrDefault(c => c.ProductId == id);
            if (item != null)
                item.Quantity++;
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = id,
                    Quantity = 1
                });
            }

            Cart = cart;
        }

        public CartViewModel TransformCart()
        {
            var products = _productService.GetProducts(new ProductsFilter
            {
                Ids = Cart.CartItems.Select(item => item.ProductId).ToList()
            }).Select(p => new ProductViewModel
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                BrandName = p.Brand != null
                    ? p.Brand.Name
                    : string.Empty
            }).ToList();

            var r = new CartViewModel
            {
                Items = Cart.CartItems
                    .ToDictionary(c => products
                            .Single(p => p.Id == c.ProductId),
                        item => item.Quantity)
            };

            return r;
        }
    }
}

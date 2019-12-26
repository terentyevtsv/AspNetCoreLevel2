using System.Collections.Generic;
using System.Linq;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.Services
{
    public class CartService : ICartService
    {
        private readonly IProductService _productService;
        private readonly ICartStore _cartStore;


        public CartService(IProductService productService, ICartStore cartStore)
        {
            _productService = productService;
            _cartStore = cartStore;
        }

        public void DecrementFromCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.CartItems
                .SingleOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                if (item.Quantity > 0)
                    item.Quantity--;

                if (item.Quantity == 0)
                    cart.CartItems.Remove(item);
            }

            _cartStore.Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.CartItems
                .SingleOrDefault(c => c.ProductId == id);
            if (item != null)
                cart.CartItems.Remove(item);

            _cartStore.Cart = cart;
        }

        public void RemoveAll()
        {
            _cartStore.Cart.CartItems.Clear();
        }

        public void AddToCart(int id)
        {
            var cart = _cartStore.Cart;

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

            _cartStore.Cart = cart;
        }

        public CartViewModel TransformCart()
        {
            var products = _productService.GetProducts(new ProductsFilter
            {
                Ids = _cartStore.Cart.CartItems.Select(item => item.ProductId).ToList()
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
                Items = _cartStore.Cart.CartItems
                    .ToDictionary(c => products
                            .Single(p => p.Id == c.ProductId),
                        item => item.Quantity)
            };

            return r;
        }
    }
}

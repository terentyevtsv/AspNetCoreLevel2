using System.Collections.Generic;
using System.Linq;

namespace WebStore.ViewModels
{
    public class CartViewModel
    {
        public Dictionary<ProductViewModel, int> Items { get; set; }

        public int ItemsCount
        {
            get { return Items?.Sum(item => item.Value) ?? 0; }
        }

        private decimal GetPrice()
        {
            var price = Items
                            ?.Sum(item => item.Value * item.Key.Price)
                        ?? 0;
            return price;
        }

        public decimal Price
        {
            get
            {
                var price = GetPrice();
                return price;
            }
        }

        public decimal TotalPrice
        {
            get
            {
                var price = GetPrice();
                return (decimal)1.13 * price;
            }
        }

        public decimal Tax
        {
            get
            {
                var price = GetPrice();
                return price * (decimal) 0.13;
            }
        }
    }
}

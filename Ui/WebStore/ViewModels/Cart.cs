using System.Collections.Generic;
using System.Linq;

namespace WebStore.ViewModels
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        public List<CartItem> CartItems { get; set; }

        public int ItemsCount
        {
            get
            {
                return CartItems
                           ?.Sum(c => c.Quantity) 
                       ?? 0;
            }
        }
    }
}

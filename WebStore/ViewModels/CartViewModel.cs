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
    }
}

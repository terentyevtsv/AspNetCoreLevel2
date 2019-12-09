using System.Collections.Generic;

namespace WebStore.DomainNew.Filters
{
    public class ProductsFilter
    {
        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }

        public List<int> Ids { get; set; }
    }
}

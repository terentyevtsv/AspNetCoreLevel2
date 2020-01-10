using System.Collections.Generic;

namespace WebStore.DomainNew.ViewModels
{
    public class CategoryCompleteViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public int? CurrentParentCategoryId { get; set; }

        public int? CurrentCategoryId { get; set; }
    }
}

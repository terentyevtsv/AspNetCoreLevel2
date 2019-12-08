using System.Collections.Generic;
using WebStore.DomainNew.Entities.Base.Interfaces;

namespace WebStore.ViewModels
{
    public class CategoryViewModel : INamedEntity, IOrderedEntity
    {
        public CategoryViewModel()
        {
            ChildCategories = new List<CategoryViewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        public List<CategoryViewModel> ChildCategories { get; set; }

        public CategoryViewModel ParentCategory { get; set; }
    }
}

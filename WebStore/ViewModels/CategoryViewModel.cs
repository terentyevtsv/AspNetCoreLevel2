using System.Collections.Generic;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.ViewModels
{
    public class CategoryViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        public List<CategoryViewModel> ChildSections { get; set; }

        public CategoryViewModel ParentCategory { get; set; }
    }
}

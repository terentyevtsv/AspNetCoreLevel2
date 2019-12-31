using WebStore.DomainNew.Entities.Base.Interfaces;

namespace WebStore.DomainNew.Dto
{
    public class CategoryDto : INamedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

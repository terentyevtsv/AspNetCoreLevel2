using WebStore.DomainNew.Entities.Base.Interfaces;

namespace WebStore.DomainNew.Dto
{
    public class BrandDto : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}

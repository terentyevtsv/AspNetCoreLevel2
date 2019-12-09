using WebStore.DomainNew.Entities.Base.Interfaces;

namespace WebStore.DomainNew.ViewModels
{
    public class UserOrderViewModel : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public decimal TotalSum { get; set; }
    }
}

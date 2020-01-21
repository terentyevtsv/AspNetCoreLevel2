using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStore.DomainNew.Entities.Base.Interfaces;

namespace WebStore.DomainNew.ViewModels
{
    public class ProductViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public bool IsNew { get; set; }

        public bool IsSale { get; set; }

        public string BrandName { get; set; }

        public int? BrandId { get; set; }

        [Display(Name = "Категория")]
        public string Category { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public SelectList Categories { get; set; }

        public SelectList Brands { get; set; }
    }
}

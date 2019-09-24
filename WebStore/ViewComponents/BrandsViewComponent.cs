using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrasructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.ViewComponents
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public BrandsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = GetBrands();
            return View(brands);
        }

        private List<BrandViewModel> GetBrands()
        {
            var brands = _productService.GetBrands();
            var brandViewModels = brands.Select(b => new BrandViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Order = b.Order
            })
                .OrderBy(b => b.Order)
                .ToList();

            return brandViewModels;
        }
    }
}

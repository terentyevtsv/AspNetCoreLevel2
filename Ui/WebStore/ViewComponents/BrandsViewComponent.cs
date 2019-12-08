using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

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
            var brands = _productService
                .GetBrands()
                .ToList();

            var brandViewModels = new List<BrandViewModel>();
            foreach (var brand in brands)
            {
                var productFilter = new ProductsFilter
                {
                    BrandId = brand.Id
                };
                var products = _productService
                    .GetProducts(productFilter)
                    .ToList();

                var brandViewModel = new BrandViewModel
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Order = brand.Order,
                    ProductsCount = products.Count
                };
                brandViewModels.Add(brandViewModel);
            }

            return brandViewModels
                .OrderBy(b => b.Order)
                .ToList();
        }
    }
}

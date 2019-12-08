using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public CategoriesViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = GetCategories();
            return View(categories);
        }

        private List<CategoryViewModel> GetCategories()
        {
            var categories = _productService.GetCategories()
                .ToList();
            var parentCategories = categories
                .Where(c => !c.ParentId.HasValue)
                .ToList();

            var categoryViewModels = new List<CategoryViewModel>();
            foreach (var parentCategory in parentCategories)
            {
                var parentCategoryViewModel = new CategoryViewModel
                {
                    Id = parentCategory.Id,
                    Name = parentCategory.Name,
                    Order = parentCategory.Order,
                    ParentCategory = null
                };

                var childCategoryViewModels = categories
                    .Where(c => c.ParentId == parentCategory.Id)
                    .Select(c => new CategoryViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Order = c.Order,
                        ParentCategory = parentCategoryViewModel
                    })
                    .OrderBy(c => c.Order)
                    .ToList();

                parentCategoryViewModel.ChildCategories
                    .AddRange(childCategoryViewModels);

                categoryViewModels.Add(parentCategoryViewModel);
            }

            return categoryViewModels
                .OrderBy(c => c.Order)
                .ToList();
        }
    }
}

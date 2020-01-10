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

        public async Task<IViewComponentResult> InvokeAsync(string categoryId)
        {
            // Изначально категория не выбрана
            int.TryParse(categoryId, out var categoryIdInt);

            var categories = GetCategories(categoryIdInt, out var parentCategoryId);
            return View(new CategoryCompleteViewModel
            {
                Categories = categories,
                CurrentCategoryId = categoryIdInt,
                CurrentParentCategoryId = parentCategoryId
            });
        }

        private List<CategoryViewModel> GetCategories(int? categoryId, out int? parentCategoryId)
        {
            parentCategoryId = null;

            // Все категории
            var categories = _productService.GetCategories()
                .ToList();

            // Родительские категории
            var parentCategories = categories
                .Where(c => !c.ParentId.HasValue)
                .ToList();

            var categoryViewModels = new List<CategoryViewModel>();
            foreach (var parentCategory in parentCategories)
            {
                // Родительская категория
                var parentCategoryViewModel = new CategoryViewModel
                {
                    Id = parentCategory.Id,
                    Name = parentCategory.Name,
                    Order = parentCategory.Order,
                    ParentCategory = null
                };

                // Дочерние категории для текущей родительской
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
                foreach (var childCategoryViewModel in childCategoryViewModels)
                {
                    // Определение родительской категории. Если категория не выбрана то и родительская null
                    if (childCategoryViewModel.Id == categoryId)
                        parentCategoryId = parentCategory.Id;

                    // Формирование списка дочерних категорий для текущей родительской
                    parentCategoryViewModel.ChildCategories.Add(childCategoryViewModel);
                }

                categoryViewModels.Add(parentCategoryViewModel);
            }

            return categoryViewModels
                .OrderBy(c => c.Order)
                .ToList();
        }
    }
}

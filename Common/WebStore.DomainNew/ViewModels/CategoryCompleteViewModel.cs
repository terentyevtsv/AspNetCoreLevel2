﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.DomainNew.ViewModels
{
    public class CategoryCompleteViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public int? CurrentParentCategoryId { get; set; }

        public int? CurrentCategoryId { get; set; }
    }
}

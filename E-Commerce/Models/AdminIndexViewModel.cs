using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Models
{
    public class AdminIndexViewModel
    {
        public int ProductCategoryCount { get; set; }
        //public List<ProductCategory> TopCategories {get; set; }
        public int ProductCount { get; set; }
        public int OrderCount { get; set; }

        [Display(Name = "Category Name")]
        //public IList<string> ProductCategoryList { get; set; }
        public List<ProductCategory> ProductCategory { get; set; }
        //public int ProductCategoryId { get; set; }

        //public ProductCategory ProductCategory { get; set; }

    }
}

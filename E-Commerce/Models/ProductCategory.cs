using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Display(Name = "Categories")]
        [Required]
        public string Name { get; set; }


        //Reverse navigation property
        public List<Product> Products { get; set; }
    }
}

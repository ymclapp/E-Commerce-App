using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Models
{
    public class ProductCategory
    {
        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string Category { get; set; }
    }
}

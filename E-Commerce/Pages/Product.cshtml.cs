using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_Commerce.Pages
{
    public class ProductModel : PageModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public void OnGet(string productId, string productName)
        {
            ProductId = productId;
            ProductName = productName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_Commerce.Pages
{
    public class ProductShopModel : PageModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        //public string ProductImage { get; set; }
        public string ProductPrice { get; set;}
        //public string ProductSummary { get; set; }
        public void OnGet(string productId, string productName, string productPrice)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_Commerce.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository productRepository;
        public IList<Product> Products { get; set; }

        public IndexModel ( IProductRepository productRepository )
        {
            this.productRepository = productRepository;
        }

        public async Task OnGetAsync()
        {
            //ProductsModel productModel = new ProductsModel();
            Products = await productRepository.GetAll();           
        }
    }
}

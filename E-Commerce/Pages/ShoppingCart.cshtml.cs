using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using E_Commerce.Logic;
using E_Commerce.Models;

namespace E_Commerce.Pages
{
    public partial class ShoppingCartModel : PageModel
    {
        public void OnGet()
        {
        }
        public List<CartItem> GetShoppingCartItems()
        {
            ShoppingCartActions actions = new ShoppingCartActions();
            return actions.GetCartItems();
        }
    }
}

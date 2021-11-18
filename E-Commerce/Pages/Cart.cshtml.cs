using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using E_Commerce.Models;


namespace E_Commerce.Pages
{
    public class CartModel : PageModel
    {
        public List<Item> Cart { get; set; }
        public double Total { get; set; }
        public int Id { get; set; }
        private IList<Product> Products { get; set; }

        //public CartModel ( Product product )
        // {
        //     this.product = product;
        // }

        public void OnGet()
        {
            Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Cart");
            Total = Cart.Sum(i => i.Product.Price * i.Quantity);
        }

        public IActionResult OnGetBuyNow(int id)
        {
            var products = new Product();
            Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Cart");
            if(Cart == null)
            {
                Cart = new List<Item>();
                Cart.Add(new Item
                {
                    Product = Product.GetOne(id),
                    Quantity = 1
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", Cart);
            }
            else
            {
                int index = Exists(Cart, id);
                if(index == -1)
                {
                    Cart.Add(new Item
                    {
                        Product = Product.GetOne(id),
                        Quantity = 1
                    });
                }
                else
                {
                    Cart[index].Quantity++;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", Cart);
            }
            return RedirectToPage("Cart");
        }
        public  IActionResult OnGetDelete(int id)
        {
            Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Cart");
            int index = Exists(Cart, id);
            Cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", Cart);
            return RedirectToPage("Cart");
        }

        public IActionResult OnPostUpdate(int[] quantities )
        {
            Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Cart");
            for (var i = 0; i < Cart.Count; i++)
            {
                Cart[i].Quantity = quantities[i];
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", Cart);
            return RedirectToPage("Cart");
        }

        private int Exists(List<Item> cart, int id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}

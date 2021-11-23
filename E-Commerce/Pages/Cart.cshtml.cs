using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using E_Commerce.Models;
using E_Commerce.Data;
using E_Commerce.Services;
using E_Commerce.Models.Identity;
using System.Security.Claims;
using E_Commerce.Services.Identity;

namespace E_Commerce.Pages
{
    public class CartModel : PageModel
    {
        public List<Item> Cart { get; set; }
        public double Total { get; set; }
        public int Id { get; set; }
        public Product Products { get; set; }
        //public IList<Product> Products { get; set; }
     //   Task<UserDto> GetUser ( ClaimsPrincipal user );

        public IProductRepository productRepository;
        //IUserService userService;

        public CartModel ( IProductRepository productRepository )
        {
            this.productRepository = productRepository;
          
        }


        //public CartModel ( IList<Product> products )
        //{
        //     Products = products;
        // }

        public void OnGet()
        {
            Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Cart");
            Total = Cart.Sum(i => i.Product.Price * i.Quantity);
        }

        public async Task<IActionResult> OnGetBuyNow(int id)
        {
            var product = new Product();
            Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Cart");
            if(Cart == null)
            {
                Cart = new List<Item>();
                Cart.Add(new Item
                {
                    Product = await productRepository.GetOne(id),
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
                        Product = await productRepository.GetOne(id),
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
       // public async Task<int>GetCartQuantity()
    //    {
     //       var user = await userService.GetUser(UserClaimsPrincipal);
     //       var cartQuantity = await
     //           _context.CartItems
      //          .Where(cq =>
     //           cq.UserId == userId)
     //           .SumAsync(cq =>
     //           cq.Quantity);

   //         return cartQuantity;
   //     }

    }
}

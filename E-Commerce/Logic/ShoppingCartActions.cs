using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Logic
{

    //https://docs.microsoft.com/en-us/aspnet/web-forms/overview/getting-started/getting-started-with-aspnet-45-web-forms/shopping-cart
    public class ShoppingCartActions : IDisposable
    {
        public string ShoppingCartId { get; set; }
        private ECommerceDbContext _db = new ECommerceDbContext();
        public const string CartSessionKey = "CartId";
        public void AddToCart(int id)
        {
            //Retrieve the product from the database
            ShoppingCartId = GetCartId();

            var cartItem = _db.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == id);
            if(cartItem == null)
            {
                //Create a new cart item if no cart item exists
                cartItem = new Models.CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = id,
                    CartId = ShoppingCartId,
                    Product = _db.Product.SingleOrDefault(
                        p => p.ProductID == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                _db.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                //If the item does exist in the cart,
                //then add one to the quantity
                cartItem.Quantity++;
            }
            _db.SaveChanges();
        }
        public void Dispose()
        {
            if(_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }

        public string GetCartId()
        {
            if(HttpContext.Current.Session[CartSessionKey] == null)
            {
                if(!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))  //should this be username vs name?
                {
                    HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    //Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();

                }
            }
            return HttpContext.Current.Session[CartSessionKey].ToString();
        }
        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId();

            return _db.ShoppingCartItems
                .Where
                (c => c.CartId == ShoppingCartId).ToList();
        }

    }
}

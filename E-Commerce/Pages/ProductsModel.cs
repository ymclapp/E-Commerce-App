using E_Commerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace E_Commerce.Pages
{
    public class ProductsModel
    {
        private readonly List<Product> Product;

        public ProductsModel()
        {
            Product = new List<Product>()//need database info in here
            {
                new Product
                {
                    Name = Product.Name,
                    Summary = ProductSummary,
                    Condition = "Good",
                    Price = 20,
                }
        }
        public List<Product> findAll()
        {
            return Product;
        }
        public Product find(int id)
        {
            return Product
               .Where(p => p.Id == id).FirstOrDefault();
        }
    }
}

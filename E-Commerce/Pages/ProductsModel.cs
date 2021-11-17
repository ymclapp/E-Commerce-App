using E_Commerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace E_Commerce.Pages
{
    public class ProductsModel
    {
        private readonly List<Product> Products;

        public ProductsModel()
        {
            Products = new List<Product>()
            {
                new Product
                {
                    FakeId = "db001",
                    Name = "Warrior Heir",
                    Price = 20,
                },
                new Product
                {
                    FakeId = "db002",
                    Name = "Wizard Heir",
                    Price = 20,
                },
                new Product
                {
                    FakeId = "db003",
                    Name = "Dragon Heir",
                    Price = 20,
                }
            };
        }
        public List<Product> findAll()
        {
            return Products;
        }
        public Product find(int id)
        {
            return Products
               .Where(p => p.Id == id).FirstOrDefault();
        }
    }
}

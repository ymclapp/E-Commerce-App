using E_Commerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace E_Commerce.Pages
{
    public class ProductsModel
    {

        private readonly IList<DontUse> Products;

        public ProductsModel ( )
        {
            Products = new List<DontUse>()//need database info in here

            {
                new DontUse
                {

                    FakeId = "db001",
                    Name = "Warrior Heir",
                    Price = 20,
                },
                new DontUse
                {
                    FakeId = "db002",
                    Name = "Wizard Heir",
                    Price = 20,
                },
                new DontUse
                {
                    FakeId = "db003",
                    Name = "Dragon Heir",
                    Price = 20,
                }
            };
            // public IList<DontUse> findAll()
            // {
            //      return Product;
            //  }
            //  public DontUse find(int id)
            //   {
            //      return Product
            //          .Where(p => p.Id == id).FirstOrDefault();
            //  }
        }
    }
}

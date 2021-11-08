using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public interface IProductCategoryRepository
    {
        Task<List<ProductCategory>> GetAll ( );

    }
    public class DatabaseProductCategoryRepository : IProductCategoryRepository
    {
        public async Task<List<ProductCategory>> GetAll ( )
        {
            return new List<ProductCategory>
            {
                new ProductCategory {id = 45, Category = "Historical Romance" },
            };
        }
    }
}

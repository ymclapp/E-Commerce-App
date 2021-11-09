using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public interface IProductCategoryRepository
    {
        Task<List<ProductCategory>> GetAll ( );
        Task<List<ProductCategory>> GetNew (int count );
    }
    public class DatabaseProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ECommerceDbContext _context;

        public DatabaseProductCategoryRepository ( ECommerceDbContext context )
        {
            _context = context;
        }

        public async Task<List<ProductCategory>> GetAll ( )
        {
            //return new List<ProductCategory>
            // {
            //     new ProductCategory {Id = 45, Category = "Historical Romance" },
            // };
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<List<ProductCategory>> GetNew(int count)
        {
            return await _context.ProductCategories
                .OrderByDescending(pc => pc.Id) //sort newest to oldest
                .Take(count)
                .ToListAsync();
        }
    }
}

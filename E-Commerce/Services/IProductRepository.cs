using E_Commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public interface IProductRepository
    {
    }

    public class DatabaseProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _context;

        public DatabaseProductRepository (ECommerceDbContext context )
        {
            _context = context;
        }
    }
}

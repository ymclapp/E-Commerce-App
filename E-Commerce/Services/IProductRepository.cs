using E_Commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public interface IProductRepository
    {
        Task SetProductImage ( string url );
    }

    public class DatabaseProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _context;

        public DatabaseProductRepository (ECommerceDbContext context )
        {
            _context = context;
        }

        public async Task SetProductImage ( string url )
        {
            var product = await fileUploadService.
            product.ProductUrl = url;
            await fileUploadService.UpdateAsync(product);//need to fix for this to get the url
        }
    }
}

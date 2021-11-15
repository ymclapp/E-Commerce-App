using E_Commerce.Data;
using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public interface IProductRepository
    {
       // Task SetProductImage ( string url );
        Task<List<Product>> GetAll ( );
    }

    public class DatabaseProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _context;

        public DatabaseProductRepository (ECommerceDbContext context )
        {
           _context = context;
       }

        public Task<List<Product>> GetAll ( )
        {
            throw new NotImplementedException();
        }

        //  public async Task<List<Product>> GetAll ( )
        //  {
        //return new List<ProductCategory>
        // {
        //     new ProductCategory {Id = 45, Category = "Historical Romance" },
        // };
        //     return await _context.Product.ToListAsync();
        //}
        // public Task SetProductImage ( string url )
        //   {
        //       throw new NotImplementedException();
        //  }

        // public async Task SetProductImage ( string url )
        // {
        //      var product = await fileUploadService.product.productUrl
        //      product.ProductUrl = url;
        //      await fileUploadService.UpdateAsync(product);//need to fix for this to get the url
        //  }
    }
}

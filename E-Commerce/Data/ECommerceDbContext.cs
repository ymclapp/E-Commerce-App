using E_Commerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Data
{
    public class ECommerceDbContext : IdentityDbContext  //we used applicationuser before
    {
        public ECommerceDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Product { get; set; }
        //public DbSet<AdminIndexViewModel> AdminIndexViewModels { get; set; }
    }
}

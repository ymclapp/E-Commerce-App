using E_Commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public interface IDashboardRepository
    {
        Task<int> GetProductCategoryCount ( );
        Task<int> GetProductCount ( );
        Task<int> GetPendingOrderCount ( );
    }
    public class DashboardRepository : IDashboardRepository
    {
       
        public async Task<int> GetPendingOrderCount ( )
        {
            return (int)DateTime.Today.DayOfWeek;
        }

        public async Task<int> GetProductCategoryCount ( )
        {
            //return await _context.ProductCategories.CountAsync();
            return 12; //need to update from hard coded
        }

        public async Task<int> GetProductCount ( )
        {
            return 98;
        }
    }

}

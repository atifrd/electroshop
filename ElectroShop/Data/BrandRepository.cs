using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectroShop.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectroShop.Data
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ShopDbContext _context;
        public BrandRepository(ShopDbContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<Brand>> GetBrands()=>
        
             await _context.Brands.ToListAsync();
            
        

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;

        }
    }
}

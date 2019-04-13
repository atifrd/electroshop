using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectroShop.Dtos;
using ElectroShop.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectroShop.Data
{
    public class ProductPropertyRepository : IProductPropertyRepository
    {
        private readonly ShopDbContext _context;
        public ProductPropertyRepository(ShopDbContext context) { _context = context; }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<ProductPropertyToInsert>> GetProductProperties_toInsert(int Id,long productId)
        {
            //var x=from _Properties in _context.Properties
            //      join _ProductProperties in _context.ProductProperties on _Properties.Id equals _ProductProperties.RelatedProperty.Id into r
            //      from x in r.DefaultIfEmpty()
            //      select new
            //      {

            //      }
            
           return await _context.Properties.Where(c=>c.CategoryProperties.Any(a=>a.CategoryId==Id))
               
                
                  .Include(s => s.ProductProperties).Select(x => new ProductPropertyToInsert
                  {
                      propertyId = x.Id,
                      propertyName = x.Name,
                      PropertyValue ="",
                      productpropertyId = 0,
                      ProductId = productId

                  })
                  .ToListAsync();

        }

       

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectroShop.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectroShop.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopDbContext _context;
        public CategoryRepository(ShopDbContext context) { _context = context; }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<Category>> GetHierachyCategorie()


        {
            Action<Category> SetChildren = null;
            SetChildren = parent =>
            {

                parent.SubCategories = _context.Categories
          .Where(childItem => childItem.ParentId == parent.Id).ToList();

                //Recursively call the SetChildren method for each child.
                parent.SubCategories.ToList().ForEach(SetChildren);
            };

            //Initialize the hierarchical list to root level items
            List<Category> hierarchicalItems = await _context.Categories
                .Where(rootItem => rootItem.ParentId == null).ToListAsync();

            //Call the SetChildren method to set the children on each root level item.
            hierarchicalItems.ForEach(SetChildren);





            // var lookup = _context.Categories.ToLookup(x => x.ParentId).ToList();
            //var res = lookup[0].SelectRecursive(x => lookup[x.Id]).ToList();
            // var hierancylist = await _context.Categories.Include(e => e.SubCategories).ToListAsync();
            return hierarchicalItems;//.Select(x=>new {children=x.SubCategories });


        }
        #region Property and CategoryProperty
        public async Task<IEnumerable<Property>> GetProperties()
        {
            return await _context.Properties.ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetPropertiesByCategoryId(int Id)
        {

            var x = _context.Properties;//.Include(d => d.CategoryProperties);
            return await x.Where
                (p => p.CategoryProperties.Any(a => a.CategoryId == Id))
                .ToListAsync();
            var y = _context.Categories.Where(p => p.Id == Id).Include(d => d.CategoryProperties);
        }

        public async Task<Property> GetPropertiesById(int Id)
        {
            return await _context.Properties.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        //age category id dar CategoryProperties bashe dige nabaiad subcategory barash sabt she
        public bool isCategotyTerminal(int CategoryId)
        {
            return _context.CategoryProperties.Any(x => x.CategoryId == CategoryId);
            //Where(x => x.CategoryId == CategoryId).FirstOrDefaultAsync();


        }
        #endregion
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Category>> GetHierachyCategorie_dropdown()
        {
            
            var z= await _context.Categories
                .Where(x => ! _context.Categories.Any(a=>a.ParentId==x.Id))
                .ToListAsync();
            var y = z.FirstOrDefault();
            return z;
        }
    }
}

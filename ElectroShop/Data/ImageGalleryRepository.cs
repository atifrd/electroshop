using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectroShop.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectroShop.Data
{
    public class ImageGalleryRepository : IImageGalleryRepository
    {

        private readonly ShopDbContext _context;
        public ImageGalleryRepository(ShopDbContext context)
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

        public  async Task<IEnumerable<ImageGallery>> GetImageByRelatedProductId(Int64 RelatedProductId)
        {
            return await _context.ImageGalleries.Where(p => p.RelatedProduct.Id == RelatedProductId).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;

        }
    }
}

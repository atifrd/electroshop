using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectroShop.Models;

namespace ElectroShop.Data
{
    public interface IImageGalleryRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<ImageGallery>> GetImageByRelatedProductId(Int64 RelatedProductId);
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectroShop.Models;

namespace ElectroShop.Data
{
    public interface IProductRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int Id);
        Task<IEnumerable<Product>> GetProductsByCategory(int CategoryId);
        
    }
}

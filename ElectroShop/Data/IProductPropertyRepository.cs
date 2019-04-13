using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectroShop.Dtos;
using ElectroShop.Models;

namespace ElectroShop.Data
{
    public interface IProductPropertyRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<ProductPropertyToInsert>> GetProductProperties_toInsert(int Id,long productId);

    }
}

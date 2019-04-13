using ElectroShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Data
{
    //category categoryproperty property
   public interface ICategoryRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        #region Category
        Task<IEnumerable<Category>> GetHierachyCategorie();
        #endregion
        #region Property and CategoryProperty
        Task<IEnumerable<Property>> GetProperties();
        Task<IEnumerable<Property>> GetPropertiesByCategoryId(int Id);
        bool isCategotyTerminal(int CategoryId);
        Task<Property> GetPropertiesById(int Id);
        Task<IEnumerable<Category>> GetHierachyCategorie_dropdown();
        #endregion


    }
}

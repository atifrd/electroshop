using ElectroShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Dtos
{
    public class CategorirsForTreeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public  ICollection<CategorirsForTreeDto> SubCategories { get; set; }
        //public ICollection<CategorirsForTreeDto> children { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Dtos
{
    public class PropertiesForListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OroginalName { get; set; }
        public string Description { get; set; }
        public bool required { get; set; }
        public string ControlType { get; set; }
        public string ValueType { get; set; }

    }
}

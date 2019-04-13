
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    /// <summary>
    /// if property is dropdown then we can fill property with this options ... uses for admin in shop site or sites like sheypoer and divar (city names , car names ...)
    /// </summary>
   public class PropertyOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Value { get; set; }
        public string Key { get; set; }

        [NotMapped]
        public int PropertyId { get; set; }
        [NotMapped]
        public string PropertyName { get; set; }
        [ForeignKey("PropertyId")]
        public virtual Property RelatedProperty { get; set; }
    }
}

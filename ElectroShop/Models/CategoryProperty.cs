using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class CategoryProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DefaultValue { get; set; }

        #region Forigen Key

        //[NotMapped]
        //public string CategoryName { get; set; }
        //[NotMapped]
        //public string PropertyName { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public virtual Category RelatedCategory { get; set; }
        [ForeignKey("PropertyId")]
         public int PropertyId { get; set; }
        public virtual Property RelatedProperty { get; set; }

        #endregion Forigen Key
    }
}

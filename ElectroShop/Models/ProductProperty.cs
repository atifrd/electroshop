using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class ProductProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PropertyValue { get; set; }

        #region Forigen Key

        //[NotMapped]
        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product RelatedProduct { get; set; }

        //[NotMapped]
        public int PropertyId { get; set; }
        
        [ForeignKey("PropertyId")]
        public virtual Property RelatedProperty { get; set; }

        #endregion Forigen Key

        #region Not mapped
        [NotMapped]
        public string PropertyName { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        [NotMapped]
        public string MeasureUnitName { get; set; }
        #endregion Not mapped

    }
}

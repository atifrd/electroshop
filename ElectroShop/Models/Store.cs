using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectroShop.Models
{
    public class Store
    {
        public Store()
        {
            this.Prices = new HashSet<Price>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int Stock { get; set; }

        [Description("کمترین تعداد قابل سفارش")]
        [DefaultValue(1)]
        public int MOQ { get; set; }
        public bool IsActive { get; set; }

        #region Forigen Key

        //[NotMapped]
        //public int SellerId { get; set; }
        //[NotMapped]
        //public string SellerName { get; set; }
        //[ForeignKey("SellerId")]
        //public virtual Seller RelatedSeller { get; set; }


        [NotMapped]
        public long ProductId { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product RelatedProduct { get; set; }


        [NotMapped]
        public int? ColorId { get; set; }
        [NotMapped]
        public string ColorName { get; set; }
        [ForeignKey("ColorId")]
        public virtual Color RelatedColor { get; set; }


        [NotMapped]
        public int? GuaranteeId { get; set; }
        [NotMapped]
        public string GuaranteeName { get; set; }
        [ForeignKey("GuaranteeId")]
        public virtual Guarantee RelatedGuarantee { get; set; }

        [Description("نحوه تحویل یا بسته بندی - فله ای ، کارتنی")]
        [NotMapped]
        public int? PackageId { get; set; }
        [NotMapped]
        public string PackageName { get; set; }
        [ForeignKey("PackageId")]
        public virtual Package RelatedPackage { get; set; }
        #endregion Forigen Key

        #region Relations
        public virtual ICollection<Price> Prices { get; set; }
        #endregion Relations
    }
}
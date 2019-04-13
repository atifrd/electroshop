using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroShop.Models
{
    public class Product
    {
        public Product()
        {
            this.Images = new HashSet<ImageGallery>();
            this.Stores = new HashSet<Store>();
            this.Comments = new HashSet<Comment>();
            this.ProductProperties = new HashSet<ProductProperty>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [MaxLength(1000)]
        public string OrginalName { get; set; }
        [MaxLength(1000)]
        public string Title { get; set; }


        [Description("به زودی")]
        [DefaultValue(false)]
        public bool IsPreview { get; set; }

        [DefaultValue(true)]
        public bool IsNew { get; set; }

        [DefaultValue(true)]
        public bool IsEnabled { get; set; }

        public string Description { get; set; }

        public DateTime RegDate { get; set; }

        [DefaultValue(0)]
        public double SalesCount { get; set; }

        #region Forgien Keys

        [NotMapped]
        public int SellerId { get; set; }
        [NotMapped]
        public string SellerName { get; set; }
        [ForeignKey("SellerId")]
        public virtual Seller RelatedSeller { get; set; }


        [NotMapped]
        public int CategoryId { get; set; }
        [NotMapped]
        public string CategoryName { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category RelatedCategory { get; set; }

        [NotMapped]
        public int BrandId { get; set; }
        [NotMapped]
        public string BrandName { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand RelatedBrand { get; set; }

        #endregion Forgien Keys

        #region Relations
        public virtual ICollection<ImageGallery> Images { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ProductProperty> ProductProperties { get; set; }
        #endregion Relations

        #region Not Mapped
        [NotMapped]
        public string MainImage { get; set; }
        [NotMapped]
        public string Rating { get; set; }
        #endregion Not Mapped
    }
}

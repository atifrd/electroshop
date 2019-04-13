
namespace ElectroShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ImageGallery
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [MaxLength(1000)]
        public string Title { get; set; }
        [MaxLength(3000)]
        public string Description { get; set; }
        [MaxLength(500)]
        public string Path { get; set; }
        [MaxLength(100)]
        public string ImageType { get; set; }
        public Nullable<int> Priority { get; set; }

        [MaxLength(250)]
        public string Link { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegDate { get; set; }

        #region Forigen Key
        [NotMapped]
        public long? NewsId { get; set; }
        [Description("خبر مرتبط که می تواند نال باشد ")]
        [ColumnAttribute(Order = 12)]
        public virtual News RelatedNews { get; set; }


        [NotMapped]
        public long? ProductId { get; set; }
        [Description("محصول مرتبط که می تواند نال باشد ")]
        [ColumnAttribute(Order = 12)]
        public virtual Product RelatedProduct { get; set; }
        #endregion Forigen Key

        #region Not mapped
        [NotMapped]
        public string Thumb { get; set; }
        #endregion Not mapped
    }
}

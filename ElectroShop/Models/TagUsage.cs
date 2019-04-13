using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class TagUsage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }


        [NotMapped]
        public long TagId { get; set; }
        [NotMapped]
        public string TagName { get; set; }

        [Description(" تگ مرتبط ")]
        [ForeignKey("TagId")]
        public virtual Tag RelatedTag { get; set; }

        //------------------------------- در اینجا آی دی مرتبط با جدول مورد نظر ذخیره می شود ، ----------
        // ----- متناسب با RelatedTo ---------
        

        [NotMapped]
        public long? ProductId { get; set; }
        [NotMapped]
        public string ProductName { get; set; }

        [Description(" اگر تگ در مورد کالای خریداری شده باشد ، مقدار دارد")]
        [ForeignKey("ProductId")]
        public virtual Product RelatedProduct { get; set; }

        [NotMapped]
        public long? NewsId { get; set; }
        [NotMapped]
        public string NewsTitle { get; set; }

        [Description(" اگر تگ در مورد کالای خریداری شده باشد ، مقدار دارد")]
        [ForeignKey("NewsId")]
        public virtual News RelatedNews { get; set; }

    }
}

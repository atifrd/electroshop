using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class Tag
    {
        public Tag()
        {
            this.TagUsages = new HashSet<TagUsage>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Description("متن تگ")]
        [StringLength(1000)]
        public string TagName { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [NotMapped]
        public string RegUserId { get; set; }
        [NotMapped]
        public string RegUserFullName { get; set; }
        [Description(" شخص ثبت کننده")]
        [ForeignKey("RegUserId")]
        public virtual User RegUser { get; set; }


        [NotMapped]
        public int PortalId { get; set; }

        [Required]
        [Description("پرتال / شاید برای زبان استفاده شود یا هر چیز دیگر")]
        [ForeignKey("PortalId")]
        public virtual Portal Portal { get; set; }

        //[ForeignKey("TagId")]
        public virtual ICollection<TagUsage> TagUsages { get; set; }

        [NotMapped]
        public long UsageCount { get; set; }


    }
}

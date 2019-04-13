using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class PollTitle
    {
        public PollTitle()
        {
            this.PollResults = new HashSet<PollResult>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Description("عنوان سوال")]
        [StringLength(2000)]
        public string Title { get; set; }

        [Description("سوال مرتبط با کالا یا خبر و ...")]
        public int RelatedTo { get; set; }


        [Description("نوع پاسخ که می تواند امتیازی یا یک نظر متنی باشد")]
        [ColumnAttribute(Order = 2)]
        public bool ResponseIsScore { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsPrivate { get; set; }


        [NotMapped]
        public int PortalId { get; set; }

        [Required]
        [Description("پرتال / شاید برای زبان استفاده شود یا هر چیز دیگر")]
        [ForeignKey("PortalId")]
        public virtual Portal Portal { get; set; }

        //[ForeignKey("PollTitleId")]
        public virtual ICollection<PollResult> PollResults { get; set; }

        [NotMapped]
        public int ResultInPercent { get; set; }
    }
}

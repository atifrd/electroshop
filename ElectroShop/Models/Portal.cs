using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroShop.Models
{
    public class Portal
    {

        public Portal()
        {
            this.Users = new HashSet<User>();
            this.PollTitles = new HashSet<PollTitle>();
            this.RelatedTags = new HashSet<Tag>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Description("نام پرتال")]
        [StringLength(500)]
        public string PortalName { get; set; }

        [Required]
        [Description("فعال است؟ - پیش فرض فعال است")]
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [Required]
        [Description("زبان سایت وابسته به دامین یا زبان انتخاب شده")]
        [StringLength(100)]
        public string Language { get; set; }

        [Required]
        [Description("مشخصات زبان مانند fr-CA")]
        [StringLength(20)]//Chinese Traditional Hong Kong = zh-Hant-HK
        public string LocaleId { get; set; }

        [NotMapped]
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Portal ParentPortal { get; set; }
        public virtual ICollection<Portal> SubPortals { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<PollTitle> PollTitles { get; set; }
        public virtual ICollection<Tag> RelatedTags { get; set; }

    }
}

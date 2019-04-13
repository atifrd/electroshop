using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroShop.Models
{
    public class News
    {
        public News()
        {
            this.Images = new HashSet<ImageGallery>();
            this.Comment = new HashSet<Comment>();
            this.Tags = new HashSet<TagUsage>();
            this.PollResults = new HashSet<PollResult>();
        }

        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [MaxLength(1000)]
        public string Title { get; set; }
        public string Context { get; set; }
        [MaxLength(500)]
        public string Source { get; set; }
        [MaxLength(100)]
        public string NewsType { get; set; }
        public int CategoryId { get; set; }
        [MaxLength(100)]
        public string CategoryTags { get; set; }

        public Nullable<DateTime> PubDate { get; set; }
        public Nullable<int> Priority { get; set; }
        public string Position { get; set; }
        public Nullable<int> VisitCount { get; set; }

        [MaxLength(1000)]
        [Description("عکس اصلی خبر ، ممکن است چند عکس باشد که باید در گالری تصاویر ذخیره شود")]
        public string MainImagePath { get; set; }

        [MaxLength(250)]
        public string Links { get; set; }
        public string RelatedNewsIds { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime RegDate { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public string ImageThumb { get; set; }
        [NotMapped]
        public string PubDateFa { get; set; }


        [NotMapped]
        public string RegUserId { get; set; }
        [Required]
        [Description("کاربر ثبت کننده ")]
        [ColumnAttribute(Order = 12)]
        public virtual User RegUser { get; set; }

        [NotMapped]
        public int PortalId { get; set; }
        //[Required]
        [Description("پرتال / شاید برای زبان استفاده شود یا هر چیز دیگر")]
        [ColumnAttribute(Order = 24)]
        public virtual Portal Portal { get; set; }

        [NotMapped]
        public int LikeCount { get; set; }

        [NotMapped]
        public int DisLikeCount { get; set; }

        [NotMapped]
        public int CommentCount { get; set; }

        //[ForeignKey("NewsId")]
        public virtual ICollection<ImageGallery> Images { get; set; }
        //[ForeignKey("NewsId")]
        public virtual ICollection<Comment> Comment { get; set; }

        //[ForeignKey("NewsId")]
        public virtual ICollection<TagUsage> Tags { get; set; }

        //[ForeignKey("NewsId")]
        public virtual ICollection<PollResult> PollResults { get; set; }


    }
}

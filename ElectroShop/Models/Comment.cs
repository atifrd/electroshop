using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroShop.Models
{
    public class Comment
    {
        public Comment()
        {
            this.SubComment = new HashSet<Comment>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string CommentText { get; set; }

        [DefaultValue(false)]
        public bool CanShow { get; set; }
        public DateTime RegDate { get; set; }

        [DefaultValue(0)]
        public int Like { get; set; }
        [DefaultValue(0)]
        public int Dislike { get; set; }
        public string ip { get; set; }
        public Nullable<long> ParentId { get; set; }
        public virtual ICollection<Comment> SubComment { get; set; }

        [ForeignKey("ParentId")]
        public virtual Comment ParentComment { get; set; }

        // ----- نظر مرتبط با خبر یا محصول است---------

        [NotMapped]
        public long? ProductId { get; set; }
        [NotMapped]
        public string ProductName { get; set; }

        [Description(" اگر نظر در مورد کالای خریداری شده باشد ، مقدار دارد")]
        [ForeignKey("ProductId")]
        public virtual Product RelatedProduct { get; set; }


        [NotMapped]
        public long? NewsId { get; set; }
        [NotMapped]
        public string NewsTitle { get; set; }

        [Description(" اگر رای در مورد خبر یا مقاله باشد ، مقدار دارد")]
        [ForeignKey("NewsId")]
        public virtual News RelatedNews { get; set; }


    }
}

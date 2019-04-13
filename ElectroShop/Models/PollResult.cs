using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class PollResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Description("نظر کاربر ، که ممکن است عدد یا متن باشد")]
        [StringLength(4000)]
        public string Response { get; set; }


        //------------------------------- اگر نوع آزمون ، آزمون مترجم باشد لینک زیر ست می شود ----------

        [NotMapped]
        public long? PollTitleId { get; set; }
        [NotMapped]
        public string Title { get; set; }

        [Description(" سوال مرتبط با این نظر")]
        [ForeignKey("PollTitleId")]
        public virtual PollTitle RelatedPoll { get; set; }

        //------------------------------- در اینجا آی دی مرتبط با جدول مورد نظر ذخیره می شود ، ----------
        // ----- متناسب با RelatedTo ---------


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
        //----------------------کاربر ثبت کننده نظر-------------------------

        [NotMapped]
        public string ResponderUserId { get; set; }
        [NotMapped]
        public string ResponderUserFullName { get; set; }
        [NotMapped]
        public string ResponderUserProfileImage { get; set; }

        [Description(" شخص پاسخ دهنده")]
        [ForeignKey("ResponderUserId")]
        public virtual User ResponderUser { get; set; }

        //-------------------- توسط مدیر تائید شده یا خیر - ---------------------------
        [Required]
        [ColumnAttribute(Order = 7)]
        [DefaultValue(false)]
        public bool IsActive { get; set; }

        //----------------------فقط برای مخاطب نشان داده شود----------------------
        [Required]
        [DefaultValue(false)]
        public bool IsPrivate { get; set; }


        [Required]
        [ColumnAttribute(Order = 10)]
        public DateTime Regdate { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroShop.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Sellers = new HashSet<Seller>();
            this.StoreOrders = new HashSet<StoreOrder>();
            this.News = new HashSet<News>();
            this.Comments = new HashSet<Comment>();
            this.Payments = new HashSet<Payment>();
            this.PollsOfUser = new HashSet<PollResult>();
            this.RelatedTags = new HashSet<TagUsage>();

        }

        [Required]
        [StringLength(150)]
        public string FName { get; set; }

        [Required]
        [StringLength(300)]
        public string LName { get; set; }

        [Required]
        [StringLength(15)]
        public string Mobile { get; set; }

        [Required]
        [Description("کاربر مدیر سامانه است؟")]
        [DefaultValue(false)]
        public bool IsSupervisor { get; set; }

        [Required]
        [Description("is seller?")]
        [DefaultValue(false)]
        public bool IsSeller { get; set; }

        [Required]
        [Description("is seller approved")]
        [DefaultValue(false)]
        public bool IsActiveSeller { get; set; }


        [Description("جنسیت")]
        [ColumnAttribute(Order = 9)]
        public bool Sex { get; set; }

        public string ProfileImage { get; set; }

        [Description("کد ملی یا بین المللی")]
        [ColumnAttribute(Order = 14)]
        [StringLength(30)]
        public string NCode { get; set; }

        public DateTimeOffset? BDate { get; set; }

        [Required]
        [Description("فعال است؟")]
        [DefaultValue(false)]
        public bool IsActive { get; set; }

        public DateTimeOffset RegDate { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        #region forigen key

        [NotMapped]
        public int PortalId { get; set; }

        //[Required]
        [Description("پرتال / شاید برای زبان استفاده شود یا هر چیز دیگر")]
        [ColumnAttribute(Order = 24)]
        public virtual Portal Portal { get; set; }

        #endregion forigen key

        #region Relations
        [Description("فاکتورهای پرداخت توسط این کاربر")]
        public virtual ICollection<Payment> Payments { get; set; }


        [Description("sellers added by this user . by seller we can access to product")]
        public virtual ICollection<Seller> Sellers { get; set; }

        [Description("نظرات ثبت شده توسط این کاربر")]
        public virtual ICollection<Comment> Comments { get; set; }


        [Description("کالای خریداری شده توسط کاربر")]
        public virtual ICollection<StoreOrder> StoreOrders { get; set; }

        [Description("اخبار ثبت شده توسط کاربر")]
        public virtual ICollection<News> News { get; set; }


        [Description("نظرات کاربر در مورد خدماتی که استفاده کرده ، مانند ، خرید  یا مقاله ...")]
        //[ForeignKey("ResponderUserId")]
        public virtual ICollection<PollResult> PollsOfUser { get; set; }

        [Description("برچسب هایی که برای این کاربر تعریف کرده ...")]
        public virtual ICollection<TagUsage> RelatedTags { get; set; }

        //[Description("نظرسنجی ها در مورد این شخص که ممکن است استاد یا مترجم باشد")]
        //public virtual ICollection<PollResult> PollResults { get; set; }

        #endregion Relations

        [NotMapped]
        public string FullName { get; set; }

    }
}

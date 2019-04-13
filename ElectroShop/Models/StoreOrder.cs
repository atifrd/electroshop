using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class StoreOrder
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [Required]
        public int OrderCount { get; set; }

        public DateTime RegDate { get; set; }

        #region buy files
        [StringLength(2000)]
        public string DownloadPath { get; set; }

        [StringLength(300)]
        public string ZippPass { get; set; }

        #endregion buy files

        [StringLength(1000)]
        public string OrderDescr { get; set; }


        [DefaultValue(true)]
        public bool SendByPost { get; set; }

        #region forigen key

        //---------------------- پرداختی این آیتم از فاکتور---------------------
        [NotMapped]
        public long PaymentId { get; set; }

        [Required]
        [ColumnAttribute(Order = 21)]
        [ForeignKey("PaymentId")]
        public virtual Payment RelatedPayment { get; set; }

        //---------------------- مشخصات کالای خریداری شده---------------------
        [NotMapped]
        public long StoreId { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        [Description("اطلاعات کالا ، گارانتی ، قیمت و ...")]
        [NotMapped]
        public string SellerName { get; set; }
        [NotMapped]
        public string StoreInfo { get; set; }
        [Required]
        [Description("محصول مرتبط  ")]
        public virtual Store RelatedStore { get; set; }

        //---------------------- خریدار---------------------
        [NotMapped]
        public string BuyerUserId { get; set; }
        [NotMapped]
        public string BuyerUserFullName { get; set; }
        [Description("کاربر خریدار")]
        [ColumnAttribute(Order = 12)]
        public virtual User BuyerUser { get; set; }

        #endregion forigen key

        #region not mapped

        [NotMapped]
        [Description("فقط برای ذخیره قیمت کالا در زمان فروش - قیمت اصلی - بدون احتساب تخفیف یا ارزش افزوده - قیمت پرداختی با این قیمت متفاوت بوده و در اینویس ذخیره شده است ")]
        public decimal BasePrice { get; set; }

        [NotMapped]
        public decimal OffPercent { get; set; }
        #endregion not mapped
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class Payment
    {

        public Payment()
        {
            this.StoreOrders = new HashSet<StoreOrder>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Description("هزینه  پرداخت شده؟")]
        [DefaultValue(false)]
        public bool IsPaid { get; set; }


        [Required]
        [Description("مبلغ واریز شده به حساب که سایت را بدهکار می کند مانند ثبت نام کلاس یا خرید")]
        //ممکن است به دلیل تخفیف یا هر  چیز دیگری مبلغ کمتری نسبت به هزینه اصلی توسط کاربر پرداخت شود
        [DefaultValue(0)]
        public decimal AmountPaid_Bed { get; set; }

        [Required]
        [Description("برای پرداخت هایی که نیاز به تائید دارند ، مانند تائید استاد در کلاس خصوصی و تائید مدیر در سفارش ترجمه - تا تائید نشود فالس است و قابل پرداخت نیست")]
        [DefaultValue(false)]
        public bool IsActive { get; set; }


        [Required]
        [Description("مبلغ برداشت شده از حساب که سایت را بستانکار می کند - مثل پرداختی به فروشنده")]
        //ممکن است به دلیل تخفیف یا هر  چیز دیگری مبلغ کمتری نسبت به هزینه اصلی توسط کاربر پرداخت شود
        [DefaultValue(0)]
        public decimal AmountPaid_Bes { get; set; }


        [Required]
        [Description("مبلغ اصلی ")]
        //لزوما این مبلغ پرداخت نمی شود - ولی به عنوان مبلغ اصلی در سیستم ذخیره می گردد
        [DefaultValue(0)]
        public decimal MainPrice { get; set; }

        [Required]
        [Description("شماره رسید پرداختی")]
        [StringLength(200)]
        public string ReceiptNumber { get; set; }

        [Required]
        [Description("پرداختی یا واریزی بابت چه چیزی بوده ، هر چند که از رابطه بین جداول مشخص می شود")]
        [StringLength(200)]
        public string PayFor { get; set; }

        [Required]
        [Description("ﻛﺪ ﻣﺮﺟﻊ درﺧﻮاﺳﺖ ﭘﺮداﺧﺖ ﻛﻪ ﻫﻤﺮاه ﺑﺎ درﺧﻮاﺳﺖ  bpPayRequest  تولید و به پذیرنده اختصاص یافته است")]
        [StringLength(200)]
        public string RefId { get; set; }

        [Description("وﺿﻌﻴﺖ ﺧﺮﻳﺪ ﺑﺎ ﺗﻮﺟﻪ ﺑﻪ ﺟﺪول ﺷﻤﺎره هفت")]
        [StringLength(200)]
        public string ResCode { get; set; }

        [Description("ﺷﻤﺎره درﺧﻮاﺳﺖ ﭘﺮداﺧﺖ")]
        [StringLength(200)]
        public string SaleOrderId { get; set; }

        [Description("ﻛﺪ ﻣﺮﺟﻊ ﺗﺮاﻛﻨﺶ ﺧﺮﻳﺪ ﻛﻪ از ﺳﺎﻳﺖ ﺑﺎﻧﻚ ﺑﻪ ﭘﺬﻳﺮﻧﺪه داده ﻣﻲ ﺷﻮد ")]
        [StringLength(200)]
        public string SaleReferenceId { get; set; }

        [Required]
        [Description("بانک عامل")]
        [StringLength(200)]
        public string BankId { get; set; }

        [Description("تاریخ ثبت که با تاریخ پرداخت تفاوت دارد - در ترجمه فاکتور ثبت می شود اما پرداخت نمی شود")]
        public DateTime RegDate { get; set; }

        [Description("تصویر قبض پرداختی")]
        [StringLength(1000)]
        public string ReceiptImage { get; set; }

        [Description("تاریخ و ساعت پرداخت")]
        public DateTime PayDate { get; set; }

        [Required]
        [Description("واحد پول")]
        [StringLength(50)]
        public string Currency { get; set; }

        [Required]
        [Description("واحد پول")]
        [StringLength(50)]
        public string CurrencyIds { get; set; }

        public string Descr { get; set; }

        [Description("کنسل شده و پول برگشت داده شده؟")]
        public bool IsCanceled { get; set; }

        [Description(" توضیحات کنسلی")]
        public string CancelDescr { get; set; }

        #region checkout
        [Required]
        [Description("آیا با استاد ، مترجم ، فروشنده و ... تسویه حساب شده")]
        [DefaultValue(false)]
        public bool IsCheckout { get; set; }

        [Description("زمان پرداخت به طلبکار ، زمان تسویه حساب")]
        public DateTime? CheckoutDate { get; set; }

        [Description("اطلاعات پرداخت به فروشنده")]
        public string CheckoutInfo { get; set; }

        #endregion checkout


        [Required]
        [Description("خریدهای انجام شده از فروشگاه مرنبط با این فاکتور")]
        //[ForeignKey("PaymentId")]
        public virtual ICollection<StoreOrder> StoreOrders { get; set; }


        [NotMapped]
        public string PayerUserId { get; set; }

        [NotMapped]
        public string PayerUserName { get; set; }
        [Required]
        [Description("کاربر پرداخت کننده")]
        [ForeignKey("UserId")]
        public virtual User PayerUser { get; set; }

    }
}

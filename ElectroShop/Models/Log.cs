using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
  public class Log
  {
    public Log()
    {
      this.SubLogs = new HashSet<Log>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public string Subject { get; set; }

    [Required]
    [Description("ماژول مرتبط : لاگین ، ارتباط با کلاس ، ...")]
    public int LogModuleId { get; set; }

    [StringLength(100)]
    public string LogModuleName { get; set; }
    public string MessageUseFor { get; set; }
    public DateTime LogDate { get; set; }
    public string Ip { get; set; }
    public string ExtraInfo { get; set; }
    public string Descr { get; set; }

    [Required]
    [Description("ایمیل ارسال شود؟")]
    [DefaultValue(true)]
    public bool SendEmail { get; set; }

    [Required]
    [Description("پیامک ارسال شود؟")]  
    [DefaultValue(true)]
    public bool SendSMS { get; set; }

    [Required]
    [Description("به کاربر نشان داده شود؟")]
    [DefaultValue(true)]
    public bool ShowToUser { get; set; }

    [Description("حذف شده؟")]
    [DefaultValue(true)]
    public bool Deleted { get; set; }

    [Description("مانند ای دی کلاس در حال برگزاری یا سفارش انجام شده ، یا آی دی امتحان آزمون ثبت نام")]
    public Nullable<long> RelatedId { get; set; }

    [Description("نام بانک مرتبط با آی دی بالا")]
    public string TableName { get; set; }

    [Required]
    [ColumnAttribute(Order = 8)]
    [Description("نوع لاگ : خطا ، هشدار ، موفقیت آمیز ، اطلاعات ، ...")]
    public string LogType { get; set; }

    [NotMapped]
    public string UserId { get; set; }

    //[Required]
    [Description("کاربر لاگین شده ")]
    [ForeignKey("UserId")]
    public virtual User LoginedUser { get; set; }

    [NotMapped]
    [DefaultValue(false)]
    public Boolean SendToAdmin { get; set; }


    [NotMapped]
    public string RelatedUserId { get; set; }

    [Description("کاربر مرتبط با فعالیت ، مثلا کاربر ثبت نام کننده که حتما باید با کاربر بالا یکی باشد ، اگر نباشد تقلبی رخ داده است")]
    [ForeignKey("RelatedUserId")]
    public virtual User RelatedUser { get; set; }


    [NotMapped]
    public long? ParentId { get; set; }

    [Description("اگر این پیام در پاسخ به یک پیام باشد کلید پیام اصلی می شود والد این پیام")]
    [ForeignKey("ParentId")]
    public virtual Log ParentLog { get; set; }
    public virtual ICollection<Log> SubLogs { get; set; }

    [NotMapped]
    public string Url { get; set; }
  }
}

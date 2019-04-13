using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Services.Email
{
  public class EmailConfiguration : IEmailConfiguration
  {
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; }
    public string SmtpPassword { get {return "leeoe@admin#123";} }

    public string PopServer { get; set; }
    public int PopPort { get; set; }
    public string PopUsername { get; set; }
    public string PopPassword { get; set; }
  }
}

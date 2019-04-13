using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Services.Email
{
  public interface IEmailConfiguration
  {
    string SmtpServer { get; }
    int SmtpPort { get; }
    string SmtpUsername { get; set; }
    string SmtpPassword { get; }

    string PopServer { get; }
    int PopPort { get; }
    string PopUsername { get; }
    string PopPassword { get; }
  }
}

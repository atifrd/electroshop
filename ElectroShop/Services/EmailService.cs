using ElectroShop.Services.Email;
using ElectroShop.Jwt;
using ElectroShop.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ElectroShop.Services
{
    public interface IEmailService
    {
        Task Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
        string CreateHtmlMessage(string message, string header, string url);
        //Task InitMessageAndSend(string subject, string content, List<User> reciver, string url, LogHelper _logHelper);
        Task InitMessageAndSend(string subject, string message, List<string> content, List<User> recivers, string url);
        Task InitMessageAndSend(string subject, string message, List<string> content, User reciver, string url);
    }


    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ShopDbContext _context;

        public EmailService(ShopDbContext context, IEmailConfiguration emailConfiguration, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _emailConfiguration = emailConfiguration;
            _jwtOptions = jwtOptions.Value;
            _context = context;
        }

        public async Task InitMessageAndSend(string subject, string message, List<string> content, List<User> recivers, string url)
        {
            try
            {
                if (recivers == null)
                {
                    recivers = new List<User>();
                    recivers.AddRange(await _context.User.Where(c => c.IsSupervisor).ToListAsync());
                }
                else if (recivers.Count == 0)
                {
                    recivers.AddRange(await _context.User.Where(c => c.IsSupervisor).ToListAsync());
                }

                var seller = recivers.Where(c => !c.IsSupervisor & !c.IsSeller).FirstOrDefault();
                var buyer = recivers.Where(c => !c.IsSupervisor & c.IsSeller).FirstOrDefault();
                string stuName = "";//student != null ? (student.FName + ' ' + student.LName) : (master != null ? (master.FName + ' ' + master.LName) : recivers[0].FName + " " + recivers[0].LName);

                string tableTags = "";//GlobalFunctions.CreateHtmlTable(content, message);
                Services.Email.EmailMessage emailmessage = new Services.Email.EmailMessage();
                emailmessage.Content = CreateHtmlMessage(tableTags, stuName, url);
                emailmessage.Subject = subject;
                foreach (User user in recivers)
                    emailmessage.ToAddresses.Add(new Services.Email.EmailAddress() { Address = user.UserName, Name = user.FName + " " + user.LName });

                await Send(emailmessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InitMessageAndSend(string subject, string message, List<string> content, User reciver, string url)
        {
            try
            {
                if (reciver == null)
                {
                    throw new Exception("email recivers is empty");
                };

                string tableTags = "";//GlobalFunctions.CreateHtmlTable(content, message);
                Services.Email.EmailMessage emailmessage = new Services.Email.EmailMessage();
                emailmessage.Content = CreateHtmlMessage(tableTags, reciver.FName + " " + reciver.LName, url);
                emailmessage.Subject = subject;
                emailmessage.ToAddresses.Add(new Services.Email.EmailAddress() { Address = reciver.UserName, Name = reciver.FName + " " + reciver.LName });

                await Send(emailmessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EmailMessage> ReceiveEmail(int maxCount = 10)
        {
            using (var emailClient = new Pop3Client())
            {
                emailClient.Connect(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);

                List<EmailMessage> emails = new List<EmailMessage>();
                for (int i = 0; i < emailClient.Count && i < maxCount; i++)
                {
                    var message = emailClient.GetMessage(i);
                    var emailMessage = new EmailMessage
                    {
                        Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                        Subject = message.Subject
                    };
                    emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                }

                return emails;
            }
        }

        public async Task Send(EmailMessage emailMessage)
        {
            try
            {
                //var client = new ImapClient(new ProtocolLogger("imap.log"));

                var message = new MimeMessage();
                message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

                emailMessage.FromAddresses.Add(new Services.Email.EmailAddress() { Address = _emailConfiguration.SmtpUsername, Name = "مدیریت لیو" });
                message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

                message.Subject = emailMessage.Subject;
                message.Priority = MessagePriority.NonUrgent;
                //var c = MimeKit.Cryptography.CryptographyContext.Create("sdsd");
                //message.SignAndEncrypt(c);
                //We will say we are sending HTML. But there are options for plaintext etc. 
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = emailMessage.Content
                };

                //Be careful that the SmtpClient class is the one from Mailkit not the framework!
                //using(var emailClient = new SmtpClient())
                using (var emailClient = new SmtpClient(new ProtocolLogger("amirimap.log")))
                {
                    try
                    {
                        //emailClient.ServerCertificateValidationCallback += ValidateServerCertificate;
                        emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        //The last parameter here is to use SSL (Which you should!)
                        await emailClient.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, MailKit.Security.SecureSocketOptions.Auto);

                        //Remove any OAuth functionality as we won't be using it. 
                        emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                        emailClient.Authenticate(_emailConfiguration.SmtpUsername, @"leeoe@admin#123");

                        emailClient.Send(message);

                        emailClient.Disconnect(true);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        emailClient.Disconnect(true);
                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static bool ValidateServerCertificate(
          object sender,
          X509Certificate certificate,
          X509Chain chain,
          System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }

        public string CreateHtmlMessage(string message, string header, string url)
        {

            //jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)]

            
            var msg = string.Format(MailTags, header, message, url , (string.IsNullOrEmpty(url) ? "none" : "block"));
            // var baseUrl = "http://test2.leeoe.com";//_jwtOptions.Audience
            // var logUrl = "http://test2.leeoe.com/icons/logo-wide.png";

            var baseUrl = "http://leeoe.com";//_jwtOptions.Audience
            var logUrl = "http://leeoe.com/icons/logo-wide.png";

            msg = msg.Replace("facebook_icon", baseUrl + "/icons/facebook.png");
            msg = msg.Replace("instagram_icon", baseUrl + "/icons/instagram.png");
            msg = msg.Replace("telegram_icon", baseUrl + "/icons/telegram.png");
            msg = msg.Replace("tweeter_icon", baseUrl + "/icons/tweeter.png");
            msg = msg.Replace("plus_icon", baseUrl + "/icons/plus.png");
            msg = msg.Replace("linkdin_icon", baseUrl + "/icons/linkdin.png");
            msg = msg.Replace("youtube_icon", baseUrl + "/icons/youtube.png");
            msg = msg.Replace("logo_image", logUrl);

            return msg;
            //doc.LoadHtml(msg);
            //doc.OptionWriteEmptyNodes = true;

            //for(int i = 1; i < 5; i++)
            //{
            //  HtmlAgilityPack.HtmlNode sign = doc.GetElementbyId("ForSign_" + i.ToString());
            //  if(sign != null)
            //    doc.GetElementbyId("ForSign_" + i.ToString()).Remove();
            //}

            //HtmlAgilityPack.HtmlNode vsec = doc.GetElementbyId("votesection");
            //string votePTag =  "<p id=\"votesection\" style=\"text-align: center; font-size: 11pt; display: block; font-family: BTitr;\">\"رای کمیسیون : \"</p>";
            //var newNode = HtmlNode.CreateNode(votePTag);
            //if(vsec != null)
            //{
            //  //  vsec.ParentNode.InsertAfter(brNode, vsec);
            //  vsec.ParentNode.ReplaceChild(newNode, vsec);
            //}

            //HtmlAgilityPack.HtmlNode vsec2 = doc.GetElementbyId("votesection");
            //string BrTag = "<br />";
            //var BrNode = HtmlNode.CreateNode(BrTag);

            //var brNode = HtmlNode.CreateNode("<br />");
            //if(vsec2 != null)
            //{
            //  vsec2.OwnerDocument.OptionWriteEmptyNodes = true;
            //  vsec2.ParentNode.InsertAfter(BrNode, vsec2);
            //}

            //var htmlmain = doc.DocumentNode.InnerHtml;
            //if(HtmlNode.ElementsFlags.Where(c => c.Key == "br").Any())
            //{
            //  HtmlNode.ElementsFlags.Where(c => c.Key == "br").FirstOrDefault();
            //  HtmlNode.ElementsFlags["br"] = HtmlElementFlag.Empty;
            //}
            //else
            //  HtmlNode.ElementsFlags.Add("br", HtmlElementFlag.Empty);

            //doc.LoadHtml(htmlmain);
            //doc.OptionWriteEmptyNodes = true;
            //for (int i = 1; i < 5; i++)
            //{
            //    HtmlAgilityPack.HtmlNode sign = doc.GetElementbyId("ForSign_" + i.ToString());
            //    if (sign != null)
            //    {
            //        string PTag = sign.OuterHtml.Replace("<span", "<p").Replace("</span>", "</p>");
            //        //var newTag = HtmlNode.CreateNode(PTag);
            //        sign.ParentNode.ReplaceChild(HtmlNode.CreateNode(PTag).ParentNode, sign);
            //    }
            //}


            // doc.OptionWriteEmptyNodes = true;
            //return doc.DocumentNode.InnerHtml;
        }

        public static string MailTags
        {
            get
            {
                return @"
   <div style='background-color:#ececec;padding:0;margin:0 auto;font-weight:200;width:100%!important;font-family:Tahoma !important;'>
      <table style='table-layout:fixed;font-weight:200;' width='100%' cellspacing='0' cellpadding='0' border='0' align='center'>
         <tbody>
            <tr>
               <td align='center'>
                  <center style='width:100%'>
                     <table style='margin:0 auto;max-width:512px;font-weight:200;width:inherit;'
                            width='512' cellspacing='0' cellpadding='0' border='0' bgcolor='#FFFFFF'>
                        <tbody>
                           <tr>
                              <td style='background-color:#f3f3f3;padding:5px 12px 12px;border-bottom:1px solid #ececec' width='100%'
                                  bgcolor='#F3F3F3'>
                                 <table style='font-weight:200;width:100%!important;min-width:100%!important' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                    <tbody>
                                       <tr>
                                          <td style='padding:0 0 0 10px;padding-top:7px;font-family:Tahoma !important;height:70px; background: url(logo_image) center center no-repeat' width='100%' valign='middle' align='right'> </td>
                                       </tr>
                                    </tbody>
                                 </table>
                              </td>
                           </tr>
                           <tr>
                              <td align='left'>
                                 <table style='font-weight:200;' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                    <tbody>
                                       <tr>
                                          <td width='100%'>

                                             <table style='font-weight:200' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                                <tbody>
                                                   <tr>
                                                      <td style='background-color:#00a0dc;padding:20px 48px;color:#ffffff' bgcolor='#00A0DC' align='center'>
                                                         <table style='font-weight:200;' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                                            <tbody>
                                                               <tr>
                                                                  <td width='100%' align='center'>
                                                                     <h1 style='padding:0;margin:0;color:#ffffff;font-weight:500;font-size:20px;line-height:24px;font-family:Tahoma !important;'> {0} </h1>
                                                                  </td>
                                                               </tr>
                                                            </tbody>
                                                         </table>
                                                      </td>
                                                   </tr>
                                                   <tr>
                                                      <td style='padding:20px 0 32px 0' align='center'>
                                                         <table style='font-weight:200;' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                                            <tbody>
                                                               <tr>
                                                                  <td width='100%' align='center'>
                                                                     <a href='' style='color:#4c4c4c;white-space:normal;display:inline-block;text-decoration:none' target='_blank' data-saferedirecturl=''>
                                                                        <!--<img alt='' src=''
                                                                             style='border-radius:50%;outline:none;color:#ffffff;display:block;text-decoration:none;font-size:12px' class='CToWUd' width='72' height='72' border='0'>-->
                                                                     </a>
                                                                  </td>
                                                               </tr>
                                                               <tr>
                                                                  <td width='100%' align='center'>
                                                                     <table style='font-weight:200;' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                                                        <tbody>
                                                                           <tr>
                                                                              <td width='100%' align='center'>
                                                                                 <h4 style='padding:0;margin:12px 24px 20px 24px;color:#4c4c4c;font-weight:200;font-size:16px;line-height:24px;font-family:Tahoma !important;'>
                                                                                    {1}
                                                                                 </h4>
                                                                                 <table style='font-weight:200;' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                                                                    <tbody>
                                                                                       <tr></tr>
                                                                                    </tbody>
                                                                                 </table> 
                                                                                 <table style='font-weight:200;cellspacing=0;cellpadding=0;border=0; align=center;display:{3}'>
                                                                                    <tbody>
                                                                                       <tr>
                                                                                          <td style='font-size:16px' valign='middle' align='center'>
                                                                                             <a href='' style='color:#4c4c4c;white-space:nowrap;display:block;text-decoration:none' target='_blank' data-saferedirecturl=''>
                                                                                                <table style='font-weight:200;' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                                                                                   <tbody>
                                                                                                      <tr>
                                                                                                         <td style='border-radius:2px;background-color:#008cc9;padding:6px 16px;font-size:16px;border-color:#008cc9;border-width:1px;border-style:solid' bgcolor='#008CC9'>
                                                                                                            <a href='{2}' style='color:#ffffff;white-space:nowrap;display:block;text-decoration:none;font-family:Tahoma !important;' target='_blank' data-saferedirecturl='{2}'> مشاهده جزئیات</a>
                                                                                                         </td>
                                                                                                      </tr>
                                                                                                   </tbody>
                                                                                                </table>
                                                                                             </a>
                                                                                          </td>
                                                                                       </tr>
                                                                                    </tbody>
                                                                                 </table>
                                                                              </td>
                                                                           </tr>
                                                                        </tbody>
                                                                     </table>
                                                                  </td>
                                                               </tr>
                                                            </tbody>
                                                         </table>
                                                      </td>
                                                   </tr>
                                                </tbody>
                                             </table>

                                          </td>
                                       </tr>
                                    </tbody>
                                 </table>
                              </td>
                           </tr>
                           <tr id='icons'>
                              <td>
                                 <table style='font-weight:200' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                    <tbody>
                                       <tr>
                                          <td>&nbsp;</td>
                                          <td>&nbsp;</td>
                                       </tr>
                                       <tr>
                                          <td style='border-top:1px solid #eeeeee;height:50%'>&nbsp;</td>
                                          <td style='border-top:1px solid #eeeeee;height:50%'>&nbsp;</td>
                                       </tr>
                                    </tbody>
                                 </table>
                                 <table style='font-weight:200;' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                    <tbody>
                                       <tr>
                                          <td style='padding:0 0 24px 0' width='100%' align='center'>
                                            <div>
                                              <a href='https://www.facebook.com/leeoeducation' style='margin:5px 1px;padding: 18px;font-size: 1px;background: url(facebook_icon) center center no-repeat;background-size:32px;'></a>
                                              <a href='https://www.instagram.com/Leeo_Educational_Group/' style='margin:5px 1px;padding: 18px;font-size: 1px;background: url(instagram_icon) center center no-repeat;'></a>
                                              <a href='http://telegram.me/AmoozeshOnline' style='margin:5px 1px; padding: 18px;font-size: 1px;background: url(telegram_icon) center center no-repeat;background-size:32px;'></a>
                                              <a href='http://www.twitter.com/LeeoeEd' style='margin:5px 1px;padding: 18px;font-size: 1px;background: url(tweeter_icon) center center no-repeat;'></a>
                                              <a href='https://plus.google.com/+LeeoeEducation' style='margin:5px 1px;padding: 18px;font-size: 1px;background: url(plus_icon) center center no-repeat;'></a>
                                              <a href='https://www.linkedin.com/company/leeo-educational-group?trk=tyah&trkInfo=tarId%3A1401124194012%2Ctas%3Aleeo%2Cidx%3A1-1-1' style='margin:5px 1px;padding: 18px;font-size: 1px;background: url(linkdin_icon) center center no-repeat;'></a>
                                              <a href='https://www.youtube.com/channel/UChLWWZQXaI61cqx46_ic0Fw' style='margin:5px 1px;padding: 18px;font-size: 1px;background: url(youtube_icon) center center no-repeat;'></a>
                                            </div>
                                          </td>
                                       </tr>
                                    </tbody>
                                 </table>
                                 <table style='font-weight:200' width='100%' cellspacing='0' cellpadding='0' border='0'> <tbody> <tr> <td>&nbsp;</td> <td rowspan='2' style='color:#bbbbbb;font-size:20px;border-color:#dddddd;line-height:100%;border-radius:40px;width:40px!important;border-width:1px;border-style:solid;height:40px!important;text-align:center' width='40' align='center'>▾</td> <td>&nbsp;</td> </tr> <tr> <td style='border-top:1px solid #eeeeee;height:50%'>&nbsp;</td> <td style='border-top:1px solid #eeeeee;height:50%'>&nbsp;</td> </tr> </tbody> </table>
                              </td>
                           </tr>
                           <tr>
                              <td align='left'>
                                 <table style='padding:0 24px;color:#999999;font-weight:200;' width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#FFFFFF'>
                                    <tbody>
                                       <tr>
                                          <td width='100%' align='center'>
                                             <table style='font-weight:200;' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                                <tbody>
                                                   <tr>
                                                      <td style='padding:16px 0;text-align:center' width='100%' valign='middle' align='center'>
                                                         <a href='http://leeoe.com/' style='font-family:Tahoma !important;color:#008cc9;white-space:normal;display:inline-block;text-decoration:none;font-size:12px;line-height:16px' target='_blank'
                                                            data-saferedirecturl=''>وب سایت لیو</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href='#' style='color:#008cc9;white-space:normal;display:inline-block;font-family:Tahoma !important;text-decoration:none;font-size:12px;line-height:16px' target='_blank' data-saferedirecturl=''>Help</a>
                                                      </td>
                                                   </tr>
                                                </tbody>
                                             </table>
                                          </td>
                                       </tr>
                                       <tr>
                                          <td width='100%' align='center'>
                                             <table style='font-weight:200;' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                                <tbody>
                                                   <tr>
                                                      <td style='padding:0 0 12px 0;font-size:12px;line-height:16px;font-family:Tahoma !important;' width='100%' align='center'>
                                                         کلیه حقوق مادی و معنوی متعلق به گروه آموزشی لیو می باشد. .
                                                      </td>
                                                   </tr>
                                                </tbody>
                                             </table>
                                             <table style='font-weight:200;' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                                <tbody>
                                                   <tr> <td style='padding:0 0 8px 0' width='100%' align='center'><a href='' style='outline:none;color:#ffffff;display:block;text-decoration:none;font-size:12px' class='CToWUd' width='82' height='20' border='0'></a></td> </tr>
                                                </tbody>
                                             </table>
                                          </td>
                                       </tr>
                                    </tbody>
                                 </table>
                              </td>
                           </tr>
                        </tbody>
                     </table>
                  </center>
               </td>
            </tr>
         </tbody>
   </div>";
            }
        }
    }

}

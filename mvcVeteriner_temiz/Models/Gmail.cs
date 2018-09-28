using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace mvcVeteriner_temiz.Models
{
    public static class Gmail
    {
        public static void SendMail(string body)
        {
            var fromAddress = new MailAddress("info@gunesliveteriner.com", "İletişim Formu");
            var toAddress = new MailAddress("info@gunesliveteriner.com");
            const string subject = "İletişim Formu";
            using (var smtp = new SmtpClient
            {
                
                Host = "smtp.yandex.com.tr",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "ab12AB34")
                //trololol kısmı e-posta adresinin şifresi
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}
using CommonLayer.RequestModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Repository.Services
{
    public class EmailService
    {
        private readonly IConfiguration config;

        public EmailService(IConfiguration config)
        {
            this.config = config;
        }

        public void SendPasswordResetLinkEmail(ForgetPasswordModel resetLink)
        {
            try
            {
                string HtmlBody;
                HtmlBody = "<html><body><h1>PasswordEncryption Reset</h1>https://localhost:44329/api/User/JwtToken</body></html>";
                HtmlBody = HtmlBody.Replace("JwtToken", resetLink.JwtToken);
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(config["IssuerEmailDetail:Email"]);
                message.To.Add(new MailAddress(resetLink.Email));
                message.Subject = "Reset Password";
                message.IsBodyHtml = true;
                message.Body = HtmlBody;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(config["IssuerEmailDetail:Email"], config["IssuerEmailDetail:Password"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

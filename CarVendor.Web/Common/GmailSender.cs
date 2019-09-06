using System;
using System.Net;
using System.Net.Mail;

namespace CarVendor.mvc.Common
{
    public static class GmailSender
    {
        private static string smtpAddress = "smtp.gmail.com";
        private static int portNumber = 587;

        public static void SendEmail(string senderEmail, string password, string recieverEmail, string subject, string body, AttachmentCollection attachments)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(recieverEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                if (attachments != null && attachments.Count > 0)
                {
                    foreach (var attachment in attachments)
                    {
                        mail.Attachments.Add(attachment);
                    }
                }
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(senderEmail, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}

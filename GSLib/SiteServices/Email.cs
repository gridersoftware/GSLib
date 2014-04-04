using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mail;

namespace GSLib.SiteServices
{
    public class Email
    {
        string smtpServer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="attachmentPath"></param>
        public void SendEmailWeb(string fromAddress, string toAddress, string subject, string message, string attachmentPath = "")
        {
#warning This function uses the obsolete namespace System.Web.Mail. It is recommended that you use GSLib.SiteServices.Email.SendEmailNet() instead.
            try
            {
                MailMessage email = new MailMessage();

                email.From = fromAddress;
                email.To = toAddress;
                email.Subject = subject;
                email.Body = message;
                email.BodyFormat = MailFormat.Text;
                if (attachmentPath != "")
                    email.Attachments.Add(new MailAttachment(attachmentPath));


                SmtpMail.SmtpServer = smtpServer;
                SmtpMail.Send(email);
            }
            catch
            {
                throw;
            }
        }

        public void SendEmailNet(string fromAddress, string toAddress, string subject, string message, System.Net.NetworkCredential credentials, string attachmentPath = "")
        {
            try
            {
                System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage(fromAddress, toAddress, subject, message);
                if (attachmentPath != "")
                    email.Attachments.Add(new System.Net.Mail.Attachment(attachmentPath));
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(smtpServer);

                client.Credentials = credentials;
                client.Send(email);
            }
            catch
            {
                throw;
            }
        }
    }
}

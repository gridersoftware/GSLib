using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mail;
using GSLib.Language;

namespace GSLib.SiteServices.Email
{
    [Obsolete("This class uses .Net functions that are obsolete. It is recommended that you use GScript.SiteServices.NetMail instead.")]
    public class WebMail : Email 
    {
        public WebMail() : base()
        { }

        public WebMail(string smtpServer)
            : base(smtpServer)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="htmlMessage"></param>
        /// <param name="attachments"></param>
        public override void SendEmail(string fromAddress, string[] toAddress, string[] cc, string[] bcc, string subject, string message, bool htmlMessage, string[] attachments)
        {
            MailMessage email = new MailMessage();

            // build email information
            email.From = fromAddress;
            email.To = DelimitedStringTokenizer.BuildDelimetedList(toAddress, ';');
            email.Cc = DelimitedStringTokenizer.BuildDelimetedList(cc, ';');
            email.Bcc = DelimitedStringTokenizer.BuildDelimetedList(bcc, ';');
            email.Subject = subject;
            email.Body = message;

            if (htmlMessage)
                email.BodyFormat = MailFormat.Html;
            else
                email.BodyFormat = MailFormat.Text;

            // add attachments
            foreach (string attachment in attachments)
            {
                email.Attachments.Add(new MailAttachment(attachment));
            }

            // send email
            SmtpMail.SmtpServer = SmtpServerUri;
            SmtpMail.Send(email);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="attachmentPath"></param>
        /// <remarks>This function uses the obsolete namespace System.Web.Main. It is recommended that you use SendEmailNet() instead.</remarks>
        public void SendEmail(string fromAddress, string toAddress, string subject, string message, string attachmentPath = "")
        {
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


                SmtpMail.SmtpServer = SmtpServerUri;
                SmtpMail.Send(email);
            }
            catch
            {
                throw;
            }
        }

        
    }
}

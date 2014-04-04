using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace GSLib.SiteServices.Email
{
    public class NetMail : Email
    {
        SmtpClient smtp;

        public NetMail(string smtpServer) : base(smtpServer)
        {
            smtp = new SmtpClient(smtpServer);
        }

        public NetMail(string smtpServer, int port)
        {
            smtp = new SmtpClient(smtpServer, port);
        }

        public NetMail(string smtpServer, string userName, string password) : base(smtpServer)
        {
            smtp = new SmtpClient(smtpServer);
            smtp.Credentials = new NetworkCredential(userName, password);
        }

        public NetMail(string smtpServer, int port, string userName, string password)
            : base(smtpServer)
        {
            smtp = new SmtpClient(smtpServer);
            smtp.Credentials = new NetworkCredential(userName, password);
        }

        public override void SendEmail(string fromAddress, string[] toAddress, string[] cc, string[] bcc, string subject, string message, bool htmlMessage, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(fromAddress);
            foreach (string addr in toAddress)
            {
                msg.To.Add(new MailAddress(addr));
            }
            foreach (string addr in cc)
            {
                msg.CC.Add(new MailAddress(addr));
            }
            foreach (string addr in bcc)
            {
                msg.Bcc.Add(new MailAddress(addr));
            }
            msg.Subject = subject;
            msg.Body = message;
            foreach (string att in attachments)
            {
                msg.Attachments.Add(new Attachment(att));
            }

            smtp.Send(msg);
        }
    }
}

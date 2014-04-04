using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GSLib.SiteServices.Email
{
    public abstract class Email
    {
        /***********************************************************************
         * Private Fields
         **********************************************************************/
        /// <summary>
        /// Stores the URI of the SMTP server to use
        /// </summary>
        string smtpServerURI;

        /***********************************************************************
         * Public Properties
         **********************************************************************/
        /// <summary>
        /// Gets the URI of the SMTP server to use to send email.
        /// </summary>
        public string SmtpServerUri
        {
            get
            {
                return smtpServerURI;
            }
        }

        /***********************************************************************
         * Constructors
         **********************************************************************/
        public Email()
        {
            smtpServerURI = "";
        }

        public Email(string smtpServer)
        {
            smtpServerURI = smtpServer;
        }

        /***********************************************************************
         * Public Methods
         **********************************************************************/
        public abstract void SendEmail(string fromAddress, string[] toAddress, string[] cc, string[] bcc, string subject, string message, bool htmlMessage, string[] attachments);

        public virtual void SendEmail(string fromAddress, string toAddress, string cc, string bcc, string subject, string message, bool htmlMessage, string[] attachments)
        {
            string[] ccs = cc != "" ? new string[] { cc } : new string[0];
            string[] bccs = bcc != "" ? new string[] { bcc } : new string[0];
            SendEmail(fromAddress, new string[] { toAddress }, ccs, bccs, subject, message, htmlMessage, attachments);
        }

        public virtual void SendEmail(string fromAddress, string toAddress, string subject, string message, string attachment)
        {
            SendEmail(fromAddress, toAddress, "", "", subject, message, false, new string[] { attachment });
        }

        public virtual void SendEmail(string fromAddress, string toAddress, string subject, string message)
        {
            SendEmail(fromAddress, toAddress, "", "", subject, message, false, new string[0]);
        }
    }
}

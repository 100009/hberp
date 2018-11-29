using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Encrypt
{
    public class CMail
    {
        private string m_strFromAddress;
        private string m_strToAddress;
        private string m_strCCAddress;
        private string m_strSubject;
        private string m_strContent;
        private string m_strAttachment;
        private string m_strBccAddress;
        private string m_UserName;
        private string m_Password;

        public CMail(string m_strFromAddress,
            string m_strToAddress,
            string m_strCCAddress,
            string m_strSubject,
            string m_strContent,
            string m_strAttachment)
        {
            this.m_strFromAddress = m_strFromAddress;
            this.m_strToAddress = m_strToAddress;
            this.m_strCCAddress = m_strCCAddress;
            this.m_strSubject = m_strSubject;
            this.m_strContent = m_strContent;
            this.m_strAttachment = m_strAttachment;
        }

        public CMail(string m_strFromAddress,
            string m_strToAddress,
            string m_strCCAddress,
            string m_strBccAddress,
            string m_strSubject,
            string m_strContent,
            string m_strAttachment)
        {
            this.m_strFromAddress = m_strFromAddress;
            this.m_strToAddress = m_strToAddress;
            this.m_strCCAddress = m_strCCAddress;
            this.m_strBccAddress = m_strBccAddress;
            this.m_strSubject = m_strSubject;
            this.m_strContent = m_strContent;
            this.m_strAttachment = m_strAttachment;
        }

        public string UserName
        {
            set { m_UserName = value; }
        }

        public string Password
        {
            set { m_Password = value; }
        }

        public bool SendMail(out string emsg)
        {
            emsg = string.Empty;
            MailMessage message = new MailMessage();

            message.From = new MailAddress(m_strFromAddress);

            //message.To.Add(new MailAddress( m_strToAddress));
            message.To.Add(m_strToAddress);

            if (m_strCCAddress != "")
            {
                message.CC.Add(m_strCCAddress);
            }
            if (m_strBccAddress != null)
            {
                message.Bcc.Add(m_strBccAddress);
            }
            message.Subject = m_strSubject;
            message.Body = m_strContent;
            if (m_strAttachment != "")
            {
                string[] arr = m_strAttachment.Split(",".ToCharArray());
                for (int i = 0; i < arr.Length; i++)
                {
                    message.Attachments.Add(new Attachment(arr[i]));
                }
            }

            try
            {
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;

                message.Priority = MailPriority.High;

                SmtpClient clint = new SmtpClient("mail.dafycredit.com");//发送邮件的服务器
                clint.UseDefaultCredentials = false;
                clint.Credentials = new System.Net.NetworkCredential(m_UserName, m_Password);

                clint.DeliveryMethod = SmtpDeliveryMethod.Network;
                clint.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                emsg = ex.Message;
                return false;
            }
        }
    }
}

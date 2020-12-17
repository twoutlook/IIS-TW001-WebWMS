using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;


    public class WmsSendMail
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="smtpserver">SMTP服务器</param>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="strfrom">发件人</param>
        /// <param name="strto">收件人</param>
        /// <param name="subj">主题</param>
        /// <param name="bodys">内容</param>
        /// <param name="SendPort">端口</param>
        /// <param name="ssl">是否SSL加密</param>
        ///  <returns>string(true/错误信息)</returns>
        public static bool IntoMail(string smtpserver, string userName, string pwd, string strfrom, string strto, string subj, string bodys, int SendPort, bool ssl)
        {
            try
            {
                //System.Net.Mail.SmtpClient _smtpClient = new System.Net.Mail.SmtpClient();
                //_smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //_smtpClient.Host = smtpserver;//SMTP服务器
                //_smtpClient.Port = SendPort;//端口
                //_smtpClient.EnableSsl = ssl;//是否SSL加密
                //_smtpClient.Credentials = new System.Net.NetworkCredential(userName, pwd);
                //System.Net.Mail.MailMessage _mailMessage = new System.Net.Mail.MailMessage(strfrom, strto);
                //_mailMessage.Subject = subj;
                //_mailMessage.Body = bodys;
                //_mailMessage.BodyEncoding = System.Text.Encoding.Default;//正文编码
                ////_mailMessage.P 
                //_mailMessage.IsBodyHtml = true;//设置为HTML格式
                //_mailMessage.Priority = System.Net.Mail.MailPriority.High;//优先级
              //  _smtpClient.Send(_mailMessage);

                //设置邮件的收件人
                MailAddress from = new MailAddress(strfrom, strfrom); //邮件的发件人
                MailMessage mail = new MailMessage();
                //设置邮件的标
                mail.Subject = subj;
                mail.From = from;
                string address = "";
                string displayName = "";
                string[] mailNames = (strto + ";").Split(';');

                foreach (string name in mailNames)
                {
                    if (name != string.Empty)
                    {
                        if (name.IndexOf('<') > 0)
                        {
                            displayName = name.Substring(0, name.IndexOf('<'));
                            address = name.Substring(name.IndexOf('<') + 1).Replace('>', ' ');
                       }
                        else
                        {
                            displayName = string.Empty;
                            address = name.Substring(name.IndexOf('<') + 1).Replace('>', ' ');
                        }
                        mail.To.Add(new MailAddress(address, displayName));
                    }

                }
                //设置邮件的内容
                mail.Body = bodys;
                //设置邮件的格式
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                //设置邮件的发送级别
                mail.Priority = MailPriority.Normal;
                SmtpClient client = new SmtpClient();
                //设置用于 SMTP 事务的主机的名称，填IP地址也可以了
                client.Host =smtpserver;
                //设置用于 SMTP 事务的端口，默认的是 25
                client.Port =SendPort;
                //取或设置 Boolean 值，该值控制 DefaultCredentials 是否随请求一起发送。 
                client.UseDefaultCredentials = false;
                client.EnableSsl = ssl;
                //这里才是真正的邮箱登陆名和密码，
                client.Credentials = new System.Net.NetworkCredential(userName, pwd);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //都定义完了，正式发送了，很是简单吧！
                client.Send(mail);          
                return true;
            }
            catch(Exception err)
            {
                throw;
            }
        }
    }


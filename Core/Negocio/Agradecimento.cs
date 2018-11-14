using Core.Core;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace Core.Negocio
{
    public class Agradecimento : IStrategy
    {
        public void slipknot(EntidadeDominio entidade)
        {
            Bilhete Classe = (Bilhete)entidade;

            try
            {
                if (string.IsNullOrEmpty(Classe.Email))
                {
                    return;// "email invalido";
                }
                else
                {
                    //OpenPop.Pop3.Pop3Client cliente = new OpenPop.Pop3.Pop3Client();
                    //cliente.Authenticate("", );
                    SmtpClient c = new SmtpClient("smtp.gmail.com", 587);
                    MailAddress add = new MailAddress(Classe.Email);
                    MailMessage msg = new MailMessage();
                    msg.To.Add(add);
                    msg.From = new MailAddress("bufalos.fatecmc@gmail.com");
                    msg.IsBodyHtml = true;
                    msg.Subject = @"Agradecimento";
                    msg.Body = "obrigado por voar com a gente";
                    if (ServicePointManager.SecurityProtocol != SecurityProtocolType.Tls12)
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    c.Credentials = new System.Net.NetworkCredential("bufalos.fatecmc@gmail.com", "5djcam67");
                    c.EnableSsl = true;
                    c.Send(msg);
                }
            }
            catch
            {

            }
        }
        public string processar(EntidadeDominio entidade)
        {
            Thread th = new Thread(new ThreadStart(() => slipknot(entidade)));
            th.Start();
            return null;
        }
    }
}

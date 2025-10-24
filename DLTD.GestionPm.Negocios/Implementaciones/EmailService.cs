using DLTD.GestionPm.Dto.Request.Email;
using DLTD.GestionPm.Negocios.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Implementaciones
{
    public class EmailService: IEmailService
    {
        private readonly EmailSettings _email;

        public EmailService(IOptions<EmailSettings> options)
        {
            _email = options.Value;
        }

        public async Task SendEmail(string recipient, string subject, string body)
        {
            using (var client = new SmtpClient(_email.Server, _email.Port))
            {
                client.Credentials = new NetworkCredential(_email.User, _email.Password);
                client.EnableSsl = true;

                var email = new MailMessage(_email.User, recipient)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                await client.SendMailAsync(email);
            }
        }
    }
}

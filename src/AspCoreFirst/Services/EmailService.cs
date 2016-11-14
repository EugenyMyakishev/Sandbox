using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace AspCoreFirst.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendAsync(string email, string subject, string message)
        {
            var mess = new MimeMessage();
            mess.From.Add(new MailboxAddress("Test adress", "eugeny.myakishev@gmail.com"));
            mess.To.Add(new MailboxAddress("mr", email));
            mess.Subject = subject;
            mess.Body = new TextPart() { Text = message };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("eugeny.myakishev@gmail.com", "Ad1mT3oS2no");
                await client.SendAsync(mess);
                await client.DisconnectAsync(true);
            }
        }
    }
}

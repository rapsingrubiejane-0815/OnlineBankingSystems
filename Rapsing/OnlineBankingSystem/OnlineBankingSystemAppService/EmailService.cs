using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;

namespace OnlineBankingSystemAppService
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public void SendEmail(string username, string recipientEmail,
                              string transactionType, double amount, double newBalance)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                _configuration["EmailSettings:FromName"],
                _configuration["EmailSettings:FromEmail"]
            ));
            message.To.Add(new MailboxAddress("Account Owner", recipientEmail));
            message.Subject = "BSIT 3-2 - Transaction Notification";
            message.Body = new TextPart("plain")
            {
                Text = $"Hello {username},\n\n" +
                       $"A {transactionType} transaction was made to your account.\n\n" +
                       $"Amount: Php {amount}\n" +
                       $"New Balance: Php {newBalance}\n\n" +
                       "Thank you for banking with us."
            };

            using (var client = new SmtpClient())
            {
                client.Connect(
                    _configuration["EmailSettings:SmtpHost"],
                    int.Parse(_configuration["EmailSettings:SmtpPort"]),
                    SecureSocketOptions.StartTls
                );

                client.Authenticate(
                    _configuration["EmailSettings:Username"],
                    _configuration["EmailSettings:Password"]
                );

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}


using MimeKit;
using MailKit.Net.Smtp;

namespace EmailServiceDemo
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        public EmailService(EmailSettings settings)
        {
            _settings = settings;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string message)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_settings.SenderName, _settings.SmtpAddress));
                email.To.Add(new MailboxAddress("", toEmail));

                email.Subject = subject;
                var body = new TextPart("plain")
                {
                    Text = message
                };
                var multipart = new Multipart() { body };
                email.Body = multipart;

                using var client = new SmtpClient();
                await client.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, true);
                await client.AuthenticateAsync(_settings.SmtpAddress, _settings.SmtpPassword);
                await client.SendAsync(email);
                await client.DisconnectAsync(false);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}

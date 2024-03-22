
using MimeKit;

namespace EmailServiceDemo
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        public EmailService(EmailSettings settings)
        {
            _settings = settings;
        }

        public Task<bool> SendEmailAsync(string toEmail, string subject, string message)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_settings.SenderName, _settings.SmtpAddress));
                email.To.Add(new MailboxAddress("", toEmail));

                email.Subject = subject;
                var body = new TextPart("plain");
                var multipart = new Multipart() { body };
                email.Body = multipart;



            }
            catch { }
            throw new NotImplementedException();
        }
    }
}

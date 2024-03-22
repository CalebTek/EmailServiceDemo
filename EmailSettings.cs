namespace EmailServiceDemo
{
    public class EmailSettings
    {
        // Connection
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }

        // Authentication
        public string SmtpAddress { get; set; }
        public string SmtpPassword { get; set; }

        // Identification
        public string SenderName { get; set; } 
        public string SenderEmail { get; set; }
    }
}

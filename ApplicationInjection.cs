using Microsoft.EntityFrameworkCore;

namespace EmailServiceDemo
{
    public static class ApplicationInjection
    {
        public static void ServiceInjection(this IServiceCollection services, IConfiguration configuration)
        {
            // Settings
            var emailSettings = new EmailSettings()
            {
                SmtpHost = configuration["EmailSettings:SmtpHost"],
                SmtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]),
                SmtpAddress = configuration["EmailSettings:SmtpAddress"],
                SmtpPassword = configuration["EmailSettings:SmtpPassword"],
                SenderName = configuration["EmailSettings:SenderName"],
                SenderEmail = configuration["EmailSettings:SenderEmail"]
            };

            // Interfaces 
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton(emailSettings);
            services.AddSingleton<TokenGenerator>();
        }

        public static void DbServiceInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}

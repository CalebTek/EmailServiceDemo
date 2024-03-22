using Microsoft.AspNetCore.Mvc;

namespace EmailServiceDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string toEmail, string subject, string message)
        {
            await _emailService.SendEmailAsync(toEmail, subject, message);
            return Ok();
        }
    }
}

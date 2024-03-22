using Microsoft.AspNetCore.Mvc;

namespace EmailServiceDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IEmailService _emailService;
        private readonly TokenGenerator _tokenGenerator;

        public UserController(ApplicationContext context, 
            IEmailService service, TokenGenerator token) 
        {
            _context = context;
            _emailService = service;
            _tokenGenerator = token;
        }

        public class CreateUserRequest
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("create-user")]
        public async Task <IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                string confirmationToken = _tokenGenerator.GenerateToken();

                var User = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Password = request.Password,
                    EmailConfirmed = false,
                    ConfirmationToken = confirmationToken
                };

                await _context.Users.AddAsync(User);
                await _context.SaveChangesAsync();

                // send email confirmation
                string emailSubject = "Email Confirmation";
                string emailMessage = $"Dear {User.FirstName}, \n\nPlease confirm your email address by clicking the following token: {confirmationToken}";

                bool emailSent = await _emailService.SendEmailAsync(User.Email, emailSubject, emailMessage);
                if (!emailSent)
                {
                    return StatusCode(500, "Failed to send email confirmation");
                }
                return Ok("User created successfully, Please check you email for confirmation");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the user: {ex.Message} ");
            }
        }

        //[HttpPost("confirm-email")]
        //public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        //{

        //}
    }
}

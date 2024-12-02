using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
 
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel emailModel)
        {
            if (emailModel == null || string.IsNullOrEmpty(emailModel.To) || string.IsNullOrEmpty(emailModel.Subject) || string.IsNullOrEmpty(emailModel.Body))
            {
                return BadRequest("Invalid email data.");
            }

            try
            {
                await _emailService.SendEmailAsync(emailModel);
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

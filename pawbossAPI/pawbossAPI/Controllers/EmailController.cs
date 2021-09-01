using Microsoft.AspNetCore.Mvc;
using pawbossAPI.DBContexts;
using pawbossAPI.Model;
using pawbossAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pawbossAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly IMailService mailService;
        private PawBossContext db = new PawBossContext();

        public EmailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        // http://localhost:53024/api/email/send
        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromBody] MailRequest request)
        {
            try
            {
                if (!db.User.Any(x => x.Username.Equals(request.Username)))
                {
                    return BadRequest("Username is invalid");
                } else
                {
                    await mailService.SendEmailAsync(request);
                    return Ok(request);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

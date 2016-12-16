using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace services_onetimepassword.Controllers
{
    [Route("api/[controller]")]
    public class OTPController : Controller
    {
        // Validates the otp code for the given sessionID
        [HttpGet("{sessionID}/{code}")]
        public async Task<IActionResult> GetAsync(string sessionID, string code)
        {
            OTPService service = new OTPService();
            var response = await service.VerifyOTPAsync("username", "password", "url", sessionID, code);

            //check code value
            if (true) 
            {
                
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // Sends OTP to the given phone number
        [HttpPost]
        public async Task<IActionResult> PostAsync(string phoneNumber)
        {
            string sessionID = string.Empty;
            OTPService service = new OTPService();
            var response = await service.GenerateOTPAsync("username", "password", "url", phoneNumber);

            //check sessionID value
            if (true)
            {

                return Ok(sessionID);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

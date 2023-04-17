using API.Jwt;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthenticateController : ControllerBase
    {
        private IConfiguration _config;

        public AuthenticateController(IConfiguration config)
        {
            _config = config;
        }
        private async Task<LoginModel> AuthenticateUser(LoginModel login)
        {
            LoginModel user = null;

            if (login.UserName == "LaDosik")
            {
                user = new LoginModel { UserName = "LaDosik", Password = "12345678" };
            }
            return user;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel data)
        {
            IActionResult response = Unauthorized();
            var user = await AuthenticateUser(data);
            if (data != null)
            {
                var tokenString = JwtManager.GenerateJSONWebToken(_config);
                response = Ok(new { Token = tokenString, Message = "Success" });
            }
            return response;
        }
    }
}

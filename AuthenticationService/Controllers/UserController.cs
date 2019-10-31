using AuthenticationService.Logic.Contracts;
using Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthenticationService.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] Login loginParam)
        {
            var token = await _userService.AuthenticateAsync(loginParam.Username, loginParam.Password);

            if (token == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] Register registerParam)
        {
            var user = await _userService.RegisterUserAsync(registerParam.Username, registerParam.Password);

            if (user == null)
                return BadRequest(new { message = "The Username is already in use." });

            return Ok(user);
        }
    }
}

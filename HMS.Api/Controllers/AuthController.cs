using HMS.Models.Dtos.Identity;
using HMS.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            await _authService.Register(model);
            return Created();
        }

        //[HttpPost("registeradmin")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> RegisterAdmin([FromForm] RegistrationRequestDto model)
        //{
        //    await _authService.RegisterAdmin(model);
        //    return Ok();
        //}
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequestDto model)
        {
            var result = await _authService.Login(model);
            return Ok(result);
        }
    }
}

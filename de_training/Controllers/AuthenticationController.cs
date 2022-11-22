using de_training.DTOs;
using de_training.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace de_training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<string> LoginAsync(LoginUserDto user)
        {
            return await _authenticationService.AuthenticateUserAsync(user.Name, user.Password);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync(LoginUserDto user)
        {
            await _authenticationService.RegistrateUserAsync(user.Name, user.Password);
            return Ok();
        }
    }
}

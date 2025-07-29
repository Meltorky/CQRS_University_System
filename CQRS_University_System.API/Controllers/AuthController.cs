using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CQRS_University_System.Application.DTOs.Auth;
using CQRS_University_System.Application.Interfaces.Identity;
using CQRS_University_System.Application.Options;
using CQRS_University_System.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CQRS_University_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegiesterAsync(dto);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            if (result is null)
            {
                return BadRequest("Internal Service Down Yala !!");
            }

            return Ok(result);
        }








        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(dto);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            if (result is null)
            {
                return BadRequest("Internal Service Down Yala !!");
            }

            return Ok(result);
        }

    }
}







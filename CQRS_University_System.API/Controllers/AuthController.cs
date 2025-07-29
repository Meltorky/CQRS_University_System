using CQRS_University_System.Application.DTOs.Auth;
using CQRS_University_System.Application.Features.Auth.Commands.Login;
using CQRS_University_System.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace CQRS_University_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Registers a new user with the provided information.
        /// </summary>
        /// <returns>Returns the authentication result including JWT token if successful.</returns>
        /// <response code="200">Returns the authentication result.</response>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterDTO dto, CancellationToken token)
        {
            var command = new RegisterCommand { registerDTO = dto};
            var result = await _mediator.Send(command ,token);
            return Ok(result);
        }


        /// <summary>
        /// Authenticates a user with email and password.
        /// </summary>
        /// <returns>Returns the authentication result including JWT token if credentials are valid.</returns>
        /// <response code="200">Returns the authentication result.</response>
        /// <response code="404">If login fails due to invalid credentials.</response>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO dto ,CancellationToken token)
        {
            var command = new LoginCommand { loginDTO = dto };
            var result = await _mediator.Send(command,token);
            return Ok(result);
        }

    }
}







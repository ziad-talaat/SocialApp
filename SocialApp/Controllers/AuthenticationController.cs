using Application.Common.ResultPattern;
using Application.Features.Authentication.Commands.Login;
using Application.Features.Authentication.Commands.Register;
using Application.Features.Authentication.DTOS;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IMediator _mediator) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var loginCommand=new LoginCommand(loginDto);
           Result<LoggedUserDto> result= await _mediator.Send(loginCommand);
            if(result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var registerCommand = new RegisterCommand(registerDto);
            Result<LoggedUserDto> result = await _mediator.Send(registerCommand);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
    }
}

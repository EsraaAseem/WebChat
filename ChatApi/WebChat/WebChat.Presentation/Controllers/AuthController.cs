using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebChat.Presentation.Abstractions;
using WebChat.Application.Cqrs.Authentication.Commands.Register;
using WebChat.Application.Cqrs.Authentication.Qureies.Login;
using WebChat.Domain.Shared;

namespace WebChat.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {     
        public AuthController(ISender sender) : base(sender)
        {
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterCommand command, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid) {
              var res=await  BaseResponse.BadRequestResponsAsync("model not valid");
                return Ok(res);
            }
            var result = await Sender.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery query, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(query, cancellationToken);
            return Ok(result);
        }

        
    }
}


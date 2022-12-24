using JWTApp.BackOffice.Core.DTOs;
using JWTApp.BackOffice.Core.Features.CQRS.Commands;
using JWTApp.BackOffice.Core.Features.CQRS.Queries;
using JWTApp.BackOffice.Infrastructure.Tools;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTApp.BackOffice.Controllers
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

        /*
            1 - User Register => member rolü ile beraber register edilecek.
            2 - Username ve password doğruysa token döneceğim.
         */
        // api/Auth/Register
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterUserCommandRequest request)
        {
            await _mediator.Send(request);
            return Created("", request);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(CheckUserQueryRequest request)
        {
            var userDto = await _mediator.Send(request);
            if (userDto != null && userDto.IsExist) 
            {
                //kullanıcı var artık token yaratmalıyım

                var token = JWTGenerator.GenerateToken(userDto);

                return Created("", token);
            }

            return BadRequest("Username or password is invalid");
        }
    }
}

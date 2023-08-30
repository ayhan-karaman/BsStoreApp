using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AuthenticationController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
             var result =  await  _serviceManager.AuthenticationService.RegisterUserAsync(userForRegistrationDto);    
             
             if(!result.Succeeded)
             {
                  foreach (var error in result.Errors)
                  {
                     ModelState.TryAddModelError(error.Code, error.Description);
                  }
                  return BadRequest(ModelState);
             }
             return StatusCode(201);
        }

        [HttpPost("Login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if(!await _serviceManager.AuthenticationService.ValidateUser(user))
                return Unauthorized();
            var tokenDto = await _serviceManager.AuthenticationService.CreateTokenAsync(populateExp : true);
            return Ok(tokenDto);
        }

        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
             var tokenDtoToReturn = await _serviceManager.AuthenticationService.RefreshTokenAsync(tokenDto);
             return Ok(tokenDtoToReturn);

        }
    }
}
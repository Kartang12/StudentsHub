using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Contracts.V1.Responses;
using News.Services;

namespace News.Controllers.V1
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticationResult
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var authResult = await _identityService.RegisterAsync(request.Email, request.Name, request.Password, request.Role, request.FormId, request.SubjectIds);

            if (!authResult.Success)
            {
                return BadRequest(new AuthenticationResult
                {
                    Errors = authResult.Errors
                });
            }

            return Ok(authResult);
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthenticationResult
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(authResponse);
        }
    }
}
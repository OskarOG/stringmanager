using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StringManager.API.V1.Messages;
using StringManager.Application.Services.Application;
using StringManager.Application.Services.Domain;
using StringManager.Domain.Objects.Value;

namespace StringManager.API.V1.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IProblemDetailFactory _problemDetailFactory;

    public AuthController(
        IAuthenticationService authenticationService,
        IProblemDetailFactory problemDetailFactory)
    {
        _authenticationService = authenticationService;
        _problemDetailFactory = problemDetailFactory;
    }
    
    [HttpPost(Name = nameof(CreateUserToken))]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUserToken([FromBody] UserTokenRequest request)
    {
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
            return BadRequest(_problemDetailFactory.CreateProblemDetail(emailResult.Error.ProblemType));
        
        var userTokenResult = await _authenticationService.CreateUserTokenAsync(emailResult.Value, request.Password);

        if (userTokenResult.IsSuccess)
            return new CreatedResult("/api/v1/auth", new UserTokenResponse(userTokenResult.Value));
        
        if (userTokenResult.Error.IsException)
            return StatusCode(
                (int)HttpStatusCode.InternalServerError,
                _problemDetailFactory.CreateProblemDetail(userTokenResult.Error.ProblemType));

        return BadRequest(_problemDetailFactory.CreateProblemDetail(userTokenResult.Error.ProblemType));
    }
}
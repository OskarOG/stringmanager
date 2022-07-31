using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StringManager.API.Configuration.Authorization;
using StringManager.API.V1.Messages;
using StringManager.Application.Services.Application;
using StringManager.Application.Services.Domain;
using StringManager.Domain.Objects.Infrastructure;
using StringManager.Domain.Objects.Value;

namespace StringManager.API.V1.Controllers;

[Authorize(AuthorizationOptionsExtensions.RequireUserAdminPolicy)]
[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IProblemDetailFactory _problemDetailFactory;

    public UserController(
        IUserService userService,
        IProblemDetailFactory problemDetailFactory)
    {
        _userService = userService;
        _problemDetailFactory = problemDetailFactory;
    }
    
    [HttpPost(Name = nameof(CreateUser))]
    public async Task<IActionResult> CreateUser(NewUserRequest request)
    {
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return CreateBadRequest(emailResult.Error);
        }
        
        var passwordResult = Password.Create(request.Password);
        if (passwordResult.IsFailure)
        {
            return CreateBadRequest(passwordResult.Error);
        }

        var userRoleWrapper = UserRoleWrapper.Parse(request.RoleType);
        if (userRoleWrapper.IsFailure)
        {
            return CreateBadRequest(userRoleWrapper.Error);
        }

        var newUser = await _userService.CreateUserAsync(
            emailResult.Value,
            userRoleWrapper.Value.Type,
            passwordResult.Value,
            request.AccessGroupIds);
        
        throw new NotImplementedException();
    }

    private BadRequestObjectResult CreateBadRequest(Error error) =>
        BadRequest(_problemDetailFactory.CreateProblemDetail(error.ProblemType));
}
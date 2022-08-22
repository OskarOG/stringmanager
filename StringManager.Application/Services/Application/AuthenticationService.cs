using StringManager.Application.Persistence;
using StringManager.Application.Services.Domain;
using StringManager.Domain.Objects.Entity;
using StringManager.Domain.Objects.Infrastructure;
using StringManager.Domain.Objects.Value;

namespace StringManager.Application.Services.Application;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenCreationService _tokenCreationService;

    public AuthenticationService(
        IUnitOfWork unitOfWork,
        ITokenCreationService tokenCreationService)
    {
        _unitOfWork = unitOfWork;
        _tokenCreationService = tokenCreationService;
    }

    public async Task<Result<string>> CreateUserTokenAsync(Email email, string password)
    {
        var validationResult = await ValidateUserInformation(email, password);
        if (validationResult.IsFailure)
            return Result<string>.ErrorResult(validationResult.Error);

        var userToBeAuthorized = validationResult.Value;
        return Result<string>.SuccessResult(
            _tokenCreationService.CreateToken(userToBeAuthorized));
    }

    private async Task<Result<User>> ValidateUserInformation(Email email, string password)
    {
        var foundUsers = await _unitOfWork.Repository<User>()
            .GetAsync(u => u.Email.Value == email.Value);
        if (!foundUsers.Any())
            return Result<User>.ErrorResult(new Error(ProblemType.WrongUserInformation));

        if (foundUsers.Count > 1)
            throw new InvalidProgramException(
                "Multiple users was found for a single email during token creation. It should not be possible to find multiple users with the same email.");

        var userToBeAuthorized = foundUsers.First();
        return !userToBeAuthorized.Password.VerifyPassword(password)
            ? Result<User>.ErrorResult(new Error(ProblemType.WrongUserInformation))
            : Result<User>.SuccessResult(userToBeAuthorized);
    }
}
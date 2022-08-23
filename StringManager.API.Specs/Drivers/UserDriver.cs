using FluentAssertions;
using Newtonsoft.Json;
using StringManager.API.Specs.Drivers.RowObjects;
using StringManager.API.Specs.Support.Exceptions;
using StringManager.API.V1.Messages;
using StringManager.Domain.Objects.Entity;
using StringManager.Infrastructure.Persistence;

namespace StringManager.API.Specs.Drivers;

public class UserDriver : IUserDriver
{
    private readonly StringManagerDbContext _dbContext;
    private readonly IHttpClientDriver _httpClientDriver;

    private NewUserRow? _currentNewUserRequest;

    public UserDriver(StringManagerDbContext dbContext, IHttpClientDriver httpClientDriver)
    {
        _dbContext = dbContext;
        _httpClientDriver = httpClientDriver;
    }
    
    public NewUserRow CurrentNewUserRequest
    {
        get => _currentNewUserRequest ?? throw new StepMissingException("A given step to set up the CurrentNewUserRequest is missing.");
        private set => _currentNewUserRequest = value;
    }

    public IEnumerable<Guid>? NewUsersInitialAccessGroupIds { get; private set; }
    
    public Guid SignedInUserId { get; private set; }

    public void CreateUserResponseShouldContainAnId()
    {
        var response = _httpClientDriver.DeserializeContent<NewUserResponse>();

        response.Should().NotBeNull();
        response!.Id.Should().NotBeEmpty();
    }

    public void NoSignedInUser()
    {
        SignedInUserId = Guid.Empty;
    }

    public void SignInUser(string userId)
    {
        SignedInUserId = Guid.Parse(userId);
    }

    public void SaveNewUserInformation(NewUserRow request)
    {
        CurrentNewUserRequest = request;
    }

    public void SaveTheNewUsersInitialAccessGroups(IEnumerable<Guid> accessGroupIds)
    {
        NewUsersInitialAccessGroupIds = accessGroupIds;
    }

    public async Task SendNewUserRequestAsync()
    {
        if (CurrentNewUserRequest == null)
        {
            throw new StepMissingException("A given step to set up the CurrentNewUserRequest is missing.");
        }
        
        var request = new NewUserRequest(
            CurrentNewUserRequest.Email,
            CurrentNewUserRequest.Password,
            Enum.GetName(CurrentNewUserRequest.RoleType)
                ?? throw new InvalidProgramException("Unable to get role type from step data."),
            NewUsersInitialAccessGroupIds?.ToArray());

        await _httpClientDriver.SendRequestAsync(
            HttpMethod.Post,
            "/UserManagement",
            JsonConvert.SerializeObject(request));
    }

    public async Task UserDatabaseShouldContainNewInformationAsync()
    {
        var newUser = await _dbContext.Set<User>()
            .FindAsync(CurrentNewUserRequest.Email);

        newUser.Should().NotBeNull();
        newUser!.UserRole.Should().Be(CurrentNewUserRequest.RoleType);
        newUser.Password.HashedValue.Should().NotBeNullOrWhiteSpace();

        if (NewUsersInitialAccessGroupIds?.Count() > 0)
        {
            newUser.Access.Should().HaveCount(NewUsersInitialAccessGroupIds.Count());
            newUser.Access
                .Select(x => x.Id)
                .Should()
                .BeEquivalentTo(NewUsersInitialAccessGroupIds);
        }
    }

    public async Task UserDatabaseShouldNotContainTheNewUser()
    {
        var user = await _dbContext.Set<User>()
            .FindAsync(CurrentNewUserRequest.Email);

        user.Should().BeNull();
    }
}
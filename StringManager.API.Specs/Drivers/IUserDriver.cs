using StringManager.API.Specs.Drivers.RowObjects;

namespace StringManager.API.Specs.Drivers;

public interface IUserDriver
{
    Guid SignedInUserId { get; }
    
    void CreateUserResponseShouldContainAnId();
    
    void NoSignedInUser();
    
    void SignInUser(string userId);
    
    void SaveNewUserInformation(NewUserRow newUser);
    
    void SaveTheNewUsersInitialAccessGroups(IEnumerable<Guid> accessGroupIds);
    
    Task SendNewUserRequestAsync();

    Task UserDatabaseShouldContainNewInformationAsync();

    Task UserDatabaseShouldNotContainTheNewUser();
}
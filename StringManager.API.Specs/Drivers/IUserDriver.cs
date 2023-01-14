using StringManager.API.Specs.Drivers.RowObjects;

namespace StringManager.API.Specs.Drivers;

public interface IUserDriver
{
    void CreateUserResponseShouldContainAnId();
    
    void SaveNewUserInformation(NewUserRow newUser);
    
    void SaveTheNewUsersInitialAccessGroups(IEnumerable<Guid> accessGroupIds);
    
    Task SendNewUserRequestAsync();

    Task UserDatabaseShouldContainNewInformationAsync();

    Task UserDatabaseShouldNotContainTheNewUser();
}
using StringManager.API.Specs.Drivers;
using StringManager.API.Specs.Drivers.RowObjects;
using TechTalk.SpecFlow.Assist;

namespace StringManager.API.Specs.Steps;

[Binding]
public class UserBackgroundSteps
{
    private readonly DatabaseDriver _databaseDriver;

    public UserBackgroundSteps(DatabaseDriver databaseDriver)
    {
        _databaseDriver = databaseDriver;
    }
    
    [Given(@"that the following users exists")]
    public async Task GivenThatTheFollowingUsersExists(Table table)
    {
        await _databaseDriver.AddExistingUsersToDatabaseAsync(
            table.CreateSet<ExistingUserRow>());
    }

    [Given(@"that the users have the following roles")]
    public async Task GivenThatTheUsersHaveTheFollowingRoles(Table table)
    {
        await _databaseDriver.AddRolesToExistingUsersAsync(
            table.CreateSet<UserRoleRow>());
    }
}
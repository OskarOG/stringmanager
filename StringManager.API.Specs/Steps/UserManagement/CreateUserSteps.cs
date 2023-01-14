using StringManager.API.Specs.Drivers;
using StringManager.API.Specs.Drivers.RowObjects;
using TechTalk.SpecFlow.Assist;

namespace StringManager.API.Specs.Steps.UserManagement;

[Binding]
[Scope(Feature = "UserManagement/CreateUser")]
public class CreateUserSteps
{
    private readonly IUserDriver _userDriver;
    
    public CreateUserSteps(IUserDriver userDriver)
    {
        _userDriver = userDriver;
    }

    [Given(@"want to create a new user with the following information")]
    public void GivenWantToCreateANewUserWithTheFollowingInformation(Table table)
    {
        _userDriver.SaveNewUserInformation(table.CreateInstance<NewUserRow>());
    }

    [Given(@"that the new user should be included in the following access groups")]
    public void GivenThatTheNewUserShouldBeIncludedInTheFollowingAccessGroups(Table table)
    {
        _userDriver.SaveTheNewUsersInitialAccessGroups(
            table.Rows.Select(row => Guid.Parse(row.Values.First())));
    }

    [When(@"the new user request is sent")]
    public async Task WhenTheNewUserRequestIsSent()
    {
        await _userDriver.SendNewUserRequestAsync();
    }

    [Then(@"the new user is created with the expected information")]
    public async Task ThenTheNewUserIsCreatedWithTheExpectedInformation()
    {
        await _userDriver.UserDatabaseShouldContainNewInformationAsync();
    }

    [Then(@"the new users id is returned")]
    public void ThenTheNewUsersIdIsReturned()
    {
        _userDriver.CreateUserResponseShouldContainAnId();
    }

    [Then(@"a new user is not created")]
    public async Task ThenANewUserIsNotCreated()
    {
        await _userDriver.UserDatabaseShouldNotContainTheNewUser();
    }
}
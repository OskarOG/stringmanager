using StringManager.API.Specs.Drivers;
using StringManager.API.Specs.Drivers.RowObjects;
using TechTalk.SpecFlow.Assist;

namespace StringManager.API.Specs.Steps.Authentication;

[Binding]
public class UserAuthenticationSteps
{
    private readonly IAuthenticationDriver _authenticationDriver;
    
    public UserAuthenticationSteps(
        IAuthenticationDriver authenticationDriver)
    {
        _authenticationDriver = authenticationDriver;
    }

    [Given(@"that the user signs in with the following information")]
    public async Task GivenThatTheUserSignsInWithTheFollowingInformation(Table table)
    {
        _authenticationDriver.SaveUserInformation(table.CreateInstance<SignInInfoRow>());
        await _authenticationDriver.SendAuthenticationRequestAsync();
    }

    [Given(@"that no user is signed in")]
    public void GivenThatNoUserIsSignedIn()
    {
        _authenticationDriver.ClearAuthenticationToken();
    }
}
using StringManager.API.Specs.Drivers;
using StringManager.API.Specs.Drivers.RowObjects;
using TechTalk.SpecFlow.Assist;

namespace StringManager.API.Specs.Steps.Authentication;

[Binding]
[Scope(Feature = "Authentication/UserAuthentication")]
public sealed class UserAuthenticationSteps
{
    private readonly IAuthenticationDriver _authenticationDriver;

    public UserAuthenticationSteps(IAuthenticationDriver authenticationDriver)
    {
        _authenticationDriver = authenticationDriver;
    }

    [Given(@"that the user with the following information wants to sign in")]
    public void GivenThatTheUserWithTheFollowingInformationWantsToSignIn(Table table)
    {
        _authenticationDriver.SaveUserInformation(table.CreateInstance<SignInInfoRow>());
    }

    [When(@"the create token request is sent")]
    public async Task WhenTheCreateTokenRequestIsSent()
    {
        await _authenticationDriver.SendAuthenticationRequestAsync();
    }
    
    [Then(@"a valid token is returned")]
    public void ThenAValidTokenIsReturned()
    {
        _authenticationDriver.ValidateReturnedToken();
    }

    [Then(@"the token has a valid length of (.*) minutes")]
    public void ThenTheTokenHasAValidLengthOfMinutes(int minutes)
    {
        _authenticationDriver.TokenShouldBeValidForExpectedTime(minutes);
    }
    
    [Then(@"the token has the ""(.*)"" as a claim")]
    public void ThenTheTokenHasTheAsAClaim(string userRoleString)
    {
        _authenticationDriver.TokenShouldContainRoleAsClaim(userRoleString);
    }
}
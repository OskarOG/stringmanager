namespace StringManager.API.Specs.Steps.UserManagement;

[Binding]
[Scope(Feature = "UserManagement_CreateUser")]
public class CreateUserSteps
{
    [Given(@"that the user ""(.*)"" is signed in")]
    public void GivenThatTheUserIsSignedIn(string userId)
    {
        ScenarioContext.StepIsPending();
    }
}
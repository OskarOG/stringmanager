using StringManager.API.Specs.Drivers.RowObjects;

namespace StringManager.API.Specs.Drivers;

public interface IAuthenticationDriver
{
    void SaveUserInformation(SignInInfoRow signInInfoRow);
    
    Task SendAuthenticationRequestAsync();

    void TokenShouldBeValidForExpectedTime(int minutes);
    
    void TokenShouldContainRoleAsClaim(string userRoleString);
    
    void ValidateReturnedToken();
}
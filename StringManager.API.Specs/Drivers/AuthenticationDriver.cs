using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using StringManager.API.Specs.Drivers.RowObjects;
using StringManager.API.Specs.Support.Exceptions;
using StringManager.API.V1.Messages;

namespace StringManager.API.Specs.Drivers;

public class AuthenticationDriver : IAuthenticationDriver
{
    private readonly IHttpClientDriver _httpClientDriver;
    private readonly IDateTimeDriver _dateTimeDriver;

    private SignInInfoRow? _signInInfoRow;
    private ClaimsPrincipal? _tokenClaims;
    private SecurityToken? _securityToken;

    public AuthenticationDriver(
        IHttpClientDriver httpClientDriver,
        IDateTimeDriver dateTimeDriver)
    {
        _dateTimeDriver = dateTimeDriver;
        _httpClientDriver = httpClientDriver;
    }

    public ClaimsPrincipal TokenClaims =>
        _tokenClaims ?? throw new StepMissingException("A step for validating the token is missing");

    public SecurityToken SecurityToken => _securityToken ??
                                          throw new StepMissingException(
                                              "A then step for validating the token is missing");

    private SignInInfoRow SignInInformation =>
        _signInInfoRow
        ?? throw new StepMissingException("A given step to set up the SignInInformation is missing.");

    public void SaveUserInformation(SignInInfoRow signInInfoRow)
    {
        _signInInfoRow = signInInfoRow;
    }

    public async Task SendAuthenticationRequestAsync()
    {
        await _httpClientDriver.SendRequestAsync(
            HttpMethod.Post,
            "http://localhost/api/v1/auth",
            new UserTokenRequest(SignInInformation.Email, SignInInformation.Password));
    }

    public void ValidateReturnedToken()
    {
        var userTokenResponse = _httpClientDriver.DeserializeContent<UserTokenResponse>();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKeyLongerThanSomeChars"));
        _tokenClaims = new JwtSecurityTokenHandler().ValidateToken(
            userTokenResponse.Token,
            new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = "stringmanager-dev",
                ValidAudience = "stringmanager-dev",
                IssuerSigningKey = key,
                ValidateLifetime = false
            },
            out _securityToken);
    }

    public void TokenShouldBeValidForExpectedTime(int minutes)
    {
        var expectedEndDate = _dateTimeDriver.CurrentTime.AddMinutes(minutes);
        SecurityToken.ValidTo.Should().Be(expectedEndDate);
    }

    public void TokenShouldContainRoleAsClaim(string userRoleString)
    {
        TokenClaims.Claims
            .First(x => x.Type == ClaimTypes.Role)
            .Value.Should().Be(userRoleString);
    }
}
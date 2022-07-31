using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StringManager.Domain.Objects.Entity;

namespace StringManager.Application.Services.Domain;

public class TokenCreationService : ITokenCreationService
{
    private readonly TimeSpan _tokenTimeSpan = new(0, 30, 0);
    private readonly IConfiguration _configuration;

    public TokenCreationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Email.Value),
            new Claim(ClaimTypes.Role, Enum.GetName(user.UserRole)
                                       ?? throw new NullReferenceException(
                                           "The user that the token will be created for does not have a valid user role.")),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.Add(_tokenTimeSpan),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
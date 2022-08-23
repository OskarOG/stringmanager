using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;
using StringManager.Application.Services.Domain;
using StringManager.Application.Services.Infrastructure;
using StringManager.Domain.Objects.Entity;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Application.UnitTests.Services.Domain;

public class TokenCreationServiceTests
{
    [Theory, DomainAutoData]
    public void CreateToken_WithValidUser_ReturnsExpectedToken(
        User user,
        string issuer,
        string key,
        [Frozen] IConfiguration configuration,
        [Frozen] IDateTimeService dateTimeService,
        TokenCreationService sut)
    {
        // Arrange
        dateTimeService.GetUniversalTime().Returns(DateTime.Now);
        configuration["Jwt:Issuer"].Returns(issuer);
        configuration["Jwt:Key"].Returns(key);
        
        // Act
        var result = sut.CreateToken(user);
        
        // Arrange
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };

        var claimsPrincipal = new JwtSecurityTokenHandler()
            .ValidateToken(result, validationParameters, out var validatedToken);

        claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.Role).Value
            .Should().Be(Enum.GetName(user.UserRole) ?? throw new NullReferenceException());

        claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.Name).Value
            .Should().Be(user.Email.Value);

        claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value
            .Should().Be(user.Id.ToString());
    }
}
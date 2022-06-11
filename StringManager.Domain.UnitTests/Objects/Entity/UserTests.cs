using System;
using FluentAssertions;
using StringManager.Domain.Objects.Entity;
using StringManager.Domain.Objects.Value;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Domain.UnitTests.Objects.Entity;

public class UserTests
{
    [Theory, DomainAutoData]
    public void Constructor_WithValidValues_SetsExpectedProperties(
        Guid id,
        Email email,
        UserRoleType userRole,
        Password password)
    {
        // Act
        var result = new User(id, email, userRole, password);

        // Assert
        result.Id.Should().Be(id);
        result.Email.Should().Be(email);
        result.UserRole.Should().Be(userRole);
        result.Password.Should().Be(password);
        result.Access.Should().BeEmpty();
    }
}
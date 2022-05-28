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
        PersonalName name,
        Email email,
        Password password)
    {
        // Act
        var result = new User(id, name, email, password);

        // Assert
        result.Id.Should().Be(id);
        result.Name.Should().Be(name);
        result.Email.Should().Be(email);
        result.Password.Should().Be(password);
        result.Access.Should().BeEmpty();
    }
}
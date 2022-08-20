using StringManager.Domain.Objects.Value;

namespace StringManager.API.Specs.Drivers.RowObjects;

public record ExistingUserRow(
    Guid UserId,
    string Email,
    UserRoleType UserRole,
    string Password);
using StringManager.Domain.Objects.Value;

namespace StringManager.API.Specs.Drivers.RowObjects;

public record UserRoleRow(Guid UserId, UserRoleType RoleType);
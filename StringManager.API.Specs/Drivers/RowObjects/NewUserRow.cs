using StringManager.Domain.Objects.Value;

namespace StringManager.API.Specs.Drivers.RowObjects;

public record NewUserRow(string Email, string Password, UserRoleType RoleType);
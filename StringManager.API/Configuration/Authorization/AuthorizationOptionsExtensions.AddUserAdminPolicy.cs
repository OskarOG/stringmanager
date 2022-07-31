using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using StringManager.Domain.Objects.Value;

namespace StringManager.API.Configuration.Authorization;

internal static partial class AuthorizationOptionsExtensions
{
    internal const string RequireUserAdminPolicy = "RequireUserAdminPolicy";
    
    internal static AuthorizationOptions AddUserAdminPolicy(this AuthorizationOptions options)
    {
        options.AddPolicy(
            RequireUserAdminPolicy,
            builder =>
            {
                builder.RequireClaim(
                    ClaimTypes.Role,
                    Enum.GetName(UserRoleType.UserAdmin)
                    ?? throw new NullReferenceException("Unable to get enum name for UserAdmin."),
                    Enum.GetName(UserRoleType.Administrator)
                    ?? throw new NullReferenceException("Unable to get enum name for Administrator."));
            });
        
        return options;
    }
}
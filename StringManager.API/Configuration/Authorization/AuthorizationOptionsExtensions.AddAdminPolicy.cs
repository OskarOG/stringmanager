using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using StringManager.Domain.Objects.Value;

namespace StringManager.API.Configuration.Authorization;

internal static partial class AuthorizationOptionsExtensions
{
    internal const string RequireAdminPolicy = "RequireAdminPolicy";
    
    internal static AuthorizationOptions AddAdminPolicy(this AuthorizationOptions options)
    {
        options.AddPolicy(RequireAdminPolicy, builder =>
        {
            builder.RequireClaim(
                ClaimTypes.Role,
                Enum.GetName(UserRoleType.Administrator)
                ?? throw new NullReferenceException("Unable to get enum name for Administrator."));
        });
        
        return options;
    }
}
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using StringManager.Domain.Objects.Value;

namespace StringManager.API.Configuration.Authorization;

internal static partial class AuthorizationOptionsExtensions
{
    internal const string RequireFolderAdminPolicy = "RequireFolderAdminPolicy";

    public static AuthorizationOptions AddFolderAdminPolicy(this AuthorizationOptions options)
    {
        options.AddPolicy(RequireFolderAdminPolicy, builder =>
        {
            builder.RequireClaim(
                ClaimTypes.Role,
                Enum.GetName(UserRoleType.FolderAdmin)
                ?? throw new NullReferenceException("Unable to get enum name for FolderAdmin."),
                Enum.GetName(UserRoleType.Administrator)
                ?? throw new NullReferenceException("Unable to get enum name for Administrator."));
        });

        return options;
    }
}
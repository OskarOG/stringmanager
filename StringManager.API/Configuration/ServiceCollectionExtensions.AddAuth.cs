using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StringManager.API.Configuration.Authorization;

namespace StringManager.API.Configuration;

internal static partial class ServiceCollectionExtensions
{
    internal static void AddAuth(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddAuthorization(options =>
            {
                options.AddFolderAdminPolicy();
                options.AddUserAdminPolicy();
                options.AddAdminPolicy();
            })
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
}
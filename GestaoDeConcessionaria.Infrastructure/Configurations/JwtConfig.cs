using GestaoDeConcessionaria.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GestaoDeConcessionaria.Infrastructure.ApiConfigurations
{
    public static class JwtConfig
    {
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Jwt");
            services.Configure<JwtSettings>(section);

            var chave = Encoding.UTF8.GetBytes(section["Chave"] ?? string.Empty);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = section["Emissor"],
                    ValidAudience = section["Publico"],
                    IssuerSigningKey = new SymmetricSecurityKey(chave)
                };
            });

            return services;
        }
    }
}

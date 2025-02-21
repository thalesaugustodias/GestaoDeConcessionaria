using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace GestaoDeConcessionaria.Infrastructure.ApiConfigurations
{
    public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "SGC - Sistema Gerenciador de Veículos",
                Version = "v1",
                Description = "Desafio Técnico",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "Thales Augusto Dias",
                    Email = "thalesaugustodias98@gmail.com"
                },
                License = new Microsoft.OpenApi.Models.OpenApiLicense
                {
                    Name = "Licença",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            var xmlCommentsPath = GetXmlCommentsPath();
            if (File.Exists(xmlCommentsPath))
            {
                options.IncludeXmlComments(xmlCommentsPath);
            }
        }

        private static string GetXmlCommentsPath()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var xmlFile = $"{assembly.GetName().Name}.xml";
            return Path.Combine(AppContext.BaseDirectory, xmlFile);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace GestaoDeConcessionaria.Infrastructure.ApiConfigurations
{
    public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IServiceProvider serviceProvider;
        public SwaggerConfigureOptions(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SGC - Sistema De Gestão de Concessionária de Veículos",
                Description = "Desafio Técnico",
                Contact = new OpenApiContact
                {
                    Name = "Thales Augusto Dias",
                    Email = "thalesaugustodias98@gmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Licença",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Utilize o esquema de autenticação Bearer para JWT. " +
                  "Insira o prefixo 'Bearer' seguido de um espaço e, em seguida, o token JWT válido. " +
                  "Exemplo: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });


            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
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

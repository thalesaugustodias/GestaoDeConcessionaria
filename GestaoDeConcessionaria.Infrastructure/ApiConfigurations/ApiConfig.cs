using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace GestaoDeConcessionaria.Infrastructure.ApiConfigurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services)
        {
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });


            services.AddHttpContextAccessor();

            services.AddMemoryCache();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IgnoreAuthorizationInDevelopment", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var httpContext = (context as AuthorizationHandlerContext)?.Resource as HttpContext;

                        if (httpContext != null)
                        {
                            var environment = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

                            return environment?.IsDevelopment() ?? false;
                        }

                        return false;
                    });
                });
            });

            return services;
        }

        public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature?.Error;

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("Ocorreu um erro interno. Por favor, tente novamente mais tarde.");
                });
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}

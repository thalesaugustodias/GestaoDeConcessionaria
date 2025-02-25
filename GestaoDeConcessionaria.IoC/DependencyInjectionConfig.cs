using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Application.Services;
using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Interfaces;
using GestaoDeConcessionaria.Infrastructure.Context;
using GestaoDeConcessionaria.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoDeConcessionaria.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<AplicationDbContext>();

            // Repositórios
            services.AddScoped<IRepository<Fabricante>, BaseRepository<Fabricante>>();
            services.AddScoped<IRepository<Veiculo>, BaseRepository<Veiculo>>();
            services.AddScoped<IRepository<Concessionaria>, BaseRepository<Concessionaria>>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IVendaRepository, VendaRepository>();
            services.AddScoped<IRelatorioService, RelatorioService>();

            // Serviços de aplicação
            services.AddScoped<IFabricanteService, FabricanteService>();
            services.AddScoped<IVeiculoService, VeiculoService>();
            services.AddScoped<IConcessionariaService, ConcessionariaService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IVendaService, VendaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddTransient<IViaCepService, ViaCepService>();

            // Identity
            services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<AplicationDbContext>()
                .AddDefaultTokenProviders();

            // HttpContextAccessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}

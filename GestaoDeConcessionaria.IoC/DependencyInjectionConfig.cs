using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Application.Services;
using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Interfaces;
using GestaoDeConcessionaria.Infrastructure.Context;
using GestaoDeConcessionaria.Infrastructure.Repository;
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
            services.AddScoped<IRepositorio<Fabricante>, RepositorioBase<Fabricante>>();
            services.AddScoped<IRepositorio<Veiculo>, RepositorioBase<Veiculo>>();
            services.AddScoped<IRepositorio<Concessionaria>, RepositorioBase<Concessionaria>>();
            services.AddScoped<IRepositorio<Cliente>, RepositorioBase<Cliente>>();
            services.AddScoped<IRepositorio<Venda>, RepositorioBase<Venda>>();

            // Serviços de aplicação
            services.AddScoped<IFabricanteService, FabricanteService>();
            services.AddScoped<IVeiculoService, VeiculoService>();
            services.AddScoped<IConcessionariaService, ConcessionariaService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IVendaService, VendaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            // Identity
            services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<AplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}

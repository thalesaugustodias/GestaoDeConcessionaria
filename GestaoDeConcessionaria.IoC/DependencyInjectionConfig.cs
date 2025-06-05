using FluentValidation;
using GestaoDeConcessionaria.Application.Common;
using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Application.Services;
using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Interfaces;
using GestaoDeConcessionaria.Domain.Notificacoes;
using GestaoDeConcessionaria.Infrastructure.Common;
using GestaoDeConcessionaria.Infrastructure.Context;
using GestaoDeConcessionaria.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();

            // Serviços de aplicação
            services.AddScoped<IFabricanteService, FabricanteService>();
            services.AddScoped<IVeiculoService, VeiculoService>();
            services.AddScoped<IConcessionariaService, ConcessionariaService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IVendaService, VendaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddTransient<IViaCepService, ViaCepService>();
            services.AddScoped<IRelatorioService, RelatorioService>();

            // Identity
            services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<AplicationDbContext>()
                .AddDefaultTokenProviders();

            // HttpContextAccessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();

            // MediatR
            var myHandlers = AppDomain.CurrentDomain.Load("GestaoDeConcessionaria.Application");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(myHandlers));

            // FluentValidation
            services.AddValidatorsFromAssembly(Assembly.Load("GestaoDeConcessionaria.Application"));

            services.AddScoped<INotificacaoRepository, NotificacaoRepository>();


            return services;
        }
    }
}

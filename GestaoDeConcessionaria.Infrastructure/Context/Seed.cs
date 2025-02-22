using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoDeConcessionaria.Infrastructure.Context
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();

            string[] roles = { "Administrador", "Vendedor", "Gerente" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                var usuarioAdmin = new Usuario
                {
                    UserName = "admin",
                    Email = "admin@teste.com",
                    NivelAcesso = NivelAcesso.Administrador
                };

                var resultado = await userManager.CreateAsync(usuarioAdmin, "Admin@123");
                if (resultado.Succeeded)
                {
                    await userManager.AddToRoleAsync(usuarioAdmin, "Administrador");
                }
                else
                {
                    throw new Exception("Falha ao criar o usuário Administrador.");
                }
            }

            var vendedorUser = await userManager.FindByNameAsync("vendedor");
            if (vendedorUser == null)
            {
                var usuarioVendedor = new Usuario
                {
                    UserName = "vendedor",
                    Email = "vendedor@teste.com",
                    NivelAcesso = NivelAcesso.Vendedor
                };

                var resultado = await userManager.CreateAsync(usuarioVendedor, "Vendedor@123");
                if (resultado.Succeeded)
                {
                    await userManager.AddToRoleAsync(usuarioVendedor, "Vendedor");
                }
                else
                {
                    throw new Exception("Falha ao criar o usuário Vendedor.");
                }
            }

            var gerenteUser = await userManager.FindByNameAsync("gerente");
            if (gerenteUser == null)
            {
                var usuarioGerente = new Usuario
                {
                    UserName = "gerente",
                    Email = "gerente@teste.com",
                    NivelAcesso = NivelAcesso.Gerente
                };

                var resultado = await userManager.CreateAsync(usuarioGerente, "Gerente@123");
                if (resultado.Succeeded)
                {
                    await userManager.AddToRoleAsync(usuarioGerente, "Gerente");
                }
                else
                {
                    throw new Exception("Falha ao criar o usuário Gerente.");
                }
            }
        }
    }
}

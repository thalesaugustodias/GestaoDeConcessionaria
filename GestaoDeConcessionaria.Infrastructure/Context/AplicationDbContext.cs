using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Infrastructure.EntityMappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeConcessionaria.Infrastructure.Context
{
    public class AplicationDbContext : IdentityDbContext<Usuario>
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }

        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Concessionaria> Concessionarias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FabricanteConfig());
            modelBuilder.ApplyConfiguration(new VeiculoConfig());
            modelBuilder.ApplyConfiguration(new ConcessionariaConfig());
            modelBuilder.ApplyConfiguration(new ClienteConfig());
            modelBuilder.ApplyConfiguration(new VendaConfig());
        }
    }
}

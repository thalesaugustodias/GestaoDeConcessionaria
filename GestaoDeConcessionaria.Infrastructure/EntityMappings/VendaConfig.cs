using GestaoDeConcessionaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoDeConcessionaria.Infrastructure.EntityMappings
{
    public class VendaConfig : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasOne(v => v.Veiculo)
                   .WithMany()
                   .HasForeignKey(v => v.VeiculoId);

            builder.HasOne(v => v.Concessionaria)
                   .WithMany()
                   .HasForeignKey(v => v.ConcessionariaId);

            builder.HasOne(v => v.Cliente)
                   .WithMany()
                   .HasForeignKey(v => v.ClienteId);
        }
    }
}

using GestaoDeConcessionaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoDeConcessionaria.Infrastructure.EntityMappings
{
    public class VeiculoConfig : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.Property(v => v.Modelo)
                   .HasMaxLength(100);

            builder.Property(v => v.Descricao)
                   .HasMaxLength(500);

            builder.HasOne(v => v.Fabricante)
                   .WithMany()
                   .HasForeignKey(v => v.FabricanteId);
        }
    }
}

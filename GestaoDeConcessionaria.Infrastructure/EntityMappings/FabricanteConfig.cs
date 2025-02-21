using GestaoDeConcessionaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoDeConcessionaria.Infrastructure.EntityMappings
{
    public class FabricanteConfig : IEntityTypeConfiguration<Fabricante>
    {
        public void Configure(EntityTypeBuilder<Fabricante> builder)
        {
            builder.HasIndex(f => f.Nome).IsUnique();
            builder.Property(f => f.Nome)
                   .HasMaxLength(100);
        }
    }
}

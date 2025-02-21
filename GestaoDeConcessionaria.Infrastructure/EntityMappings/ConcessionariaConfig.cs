using GestaoDeConcessionaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoDeConcessionaria.Infrastructure.EntityMappings
{
    public class ConcessionariaConfig : IEntityTypeConfiguration<Concessionaria>
    {
        public void Configure(EntityTypeBuilder<Concessionaria> builder)
        {
            builder.HasIndex(c => c.Nome).IsUnique();
            builder.Property(c => c.Nome)
                   .HasMaxLength(100);
        }
    }
}

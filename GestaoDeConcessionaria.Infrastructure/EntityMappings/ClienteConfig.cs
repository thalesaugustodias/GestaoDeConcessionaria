using GestaoDeConcessionaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoDeConcessionaria.Infrastructure.EntityMappings
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasIndex(c => c.CPF).IsUnique();
            builder.Property(c => c.Nome)
                   .HasMaxLength(100);
        }
    }
}

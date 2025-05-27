using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial10.Domain.Models;

namespace Tutorial10.Infrastructure.Database.Configurations;

public class MedicamentTypeConfiguration : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
       builder.ToTable("Medicament");
       builder.HasKey(m => m.IdMedicament);
       builder.Property(m => m.Type).HasMaxLength(100).IsRequired();
       builder.Property(m => m.Name).HasMaxLength(100).IsRequired();
       builder.Property(m => m.Description).HasMaxLength(100).IsRequired();
    }
}
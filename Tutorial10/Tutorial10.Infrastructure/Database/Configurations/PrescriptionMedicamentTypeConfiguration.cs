using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial10.Domain.Models;

namespace Tutorial10.Infrastructure.Database.Configurations;

public class PrescriptionMedicamentTypeConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
{
    public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
    {
       builder.ToTable("Prescription_Medicament");

       builder.HasKey(pm => new { pm.IdPrescription, pm.IdMedicament});
       
       builder.HasOne(pm => pm.Prescription)
              .WithMany(p => p.PrescriptionMedicaments)
              .HasForeignKey(pm => pm.IdPrescription);
       
       builder.HasOne(pm => pm.Medicament)
              .WithMany(m => m.PrescriptionMedicaments)
              .HasForeignKey(pm => pm.IdMedicament);

       builder.Property(pm => pm.Details).HasMaxLength(100)
                                         .IsRequired();
    }
}
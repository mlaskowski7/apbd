using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial10.Domain.Models;

namespace Tutorial10.Infrastructure.Database.Configurations;

public class PrescriptionTypeConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
       builder.ToTable("Prescription");

       builder.HasKey(p => p.IdPrescription);
       
       builder.Property(p => p.Date).IsRequired();
       builder.Property(p => p.DueDate).IsRequired();
       
       builder.HasOne(p => p.Patient)
              .WithMany(p => p.Prescriptions)
              .HasForeignKey(p => p.IdPatient);
       
       builder.HasOne(p => p.Doctor)
              .WithMany(d => d.Prescriptions)
              .HasForeignKey(p => p.IdDoctor);
    }
}
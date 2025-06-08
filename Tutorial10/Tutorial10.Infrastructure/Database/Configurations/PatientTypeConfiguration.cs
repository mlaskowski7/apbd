using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial10.Domain.Models;

namespace Tutorial10.Infrastructure.Database.Configurations;

public class PatientTypeConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
       builder.ToTable("Patient");

       builder.HasKey(p => p.IdPatient);
       
       builder.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
       builder.Property(p => p.LastName).HasMaxLength(100).IsRequired();
    }
}
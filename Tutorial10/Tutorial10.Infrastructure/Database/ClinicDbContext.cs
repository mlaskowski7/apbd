using Microsoft.EntityFrameworkCore;
using Tutorial10.Domain.Models;

namespace Tutorial10.Infrastructure.Database;

public class ClinicDbContext : DbContext 
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
    {
    }
    
    public virtual DbSet<Medicament> Medicaments { get; set; }
    
    public virtual DbSet<Prescription> Prescriptions { get; set; }
    
    public virtual DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    public virtual DbSet<Patient> Patients { get; set; }
    
    public virtual DbSet<Doctor> Doctors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicDbContext).Assembly);
    }
}
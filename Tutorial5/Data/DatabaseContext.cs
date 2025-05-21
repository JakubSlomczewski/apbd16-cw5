using Microsoft.EntityFrameworkCore;
using Tutorial5.Domain;

namespace Tutorial5.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }

    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Medicament> Medicaments { get; set; } = null!;
    public DbSet<Prescription> Prescriptions { get; set; } = null!;
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>()
            .HasKey(p => p.IdPatient);

        modelBuilder.Entity<Doctor>()
            .HasKey(d => d.IdDoctor);

        modelBuilder.Entity<Medicament>()
            .HasKey(m => m.IdMedicament);

        modelBuilder.Entity<Prescription>()
            .HasKey(p => p.IdPrescription);

        modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.PrescriptionId, pm.MedicamentId });

       
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Prescription)
            .WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.PrescriptionId);

        modelBuilder.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Medicament)
            .WithMany(m => m.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.MedicamentId);
        
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jk@przyklad.pl" },
            new Doctor { IdDoctor = 2, FirstName = "Ewa", LastName = "Nowak",   Email = "en@przyklad.pl" }
        );

        modelBuilder.Entity<Medicament>().HasData(
            new Medicament { IdMedicament = 1, Name = "Apap" },
            new Medicament { IdMedicament = 2, Name = "Ibuprom" }
        );

        
        
    }
}
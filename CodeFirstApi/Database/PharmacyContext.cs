using EntityFrameworkApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkApi.Database;

public class PharmacyContext : DbContext
{
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    public PharmacyContext(DbContextOptions<PharmacyContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Klucz złożony
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

        //Relacja jeden do wielu między Prescription a PrescriptionMedicament
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Prescription)
            .WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdPrescription);

        //Relacja jeden do wielu między Medicament a PrescriptionMedicament
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Medicament)
            .WithMany(m => m.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdMedicament);

        //Relacja jeden do wielu między Patient a Prescription
        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Patient)
            .WithMany(pa => pa.Prescriptions)
            .HasForeignKey(p => p.IdPatient);
        
        //Relacja jeden do wielu między Doctor a Prescription
        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Doctor)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(p => p.IdDoctor);

        modelBuilder.Entity<Medicament>().ToTable("Medicament", "Pharmacy");
        modelBuilder.Entity<Prescription>().ToTable("Prescription", "Pharmacy");
        modelBuilder.Entity<Patient>().ToTable("Patient", "Pharmacy");
        modelBuilder.Entity<Doctor>().ToTable("Doctor", "Pharmacy");
        modelBuilder.Entity<PrescriptionMedicament>().ToTable("Prescription_Medicament", "Pharmacy");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=Pharmacy;User Id=sa;Password=Blubber#27;Encrypt=false;TrustServerCertificate=true;");
    }
}
using EntityFrameworkApi.Models;

namespace EntityFrameworkApi.Interfaces;

public interface IPrescriptionRepository
{
    Task AddPrescriptionAsync(Prescription prescription);
    Task<Patient> GetPatientWithDetailsAsync(int patientId);
    Task<bool> MedicamentExistsAsync(int medicamentId);
}
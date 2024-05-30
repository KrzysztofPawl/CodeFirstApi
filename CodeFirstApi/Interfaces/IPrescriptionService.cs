using EntityFrameworkApi.DTO;

namespace EntityFrameworkApi.Interfaces;

public interface IPrescriptionService
{
    Task<bool> AddNewPrescriptionAsync(PrescriptionRequestDTO request);
    Task<PatientDetailsDTO> GetPatientDetailsAsync(int idPatient);
}
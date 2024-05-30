using EntityFrameworkApi.DTO;
using EntityFrameworkApi.Interfaces;
using EntityFrameworkApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkApi.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<bool> AddNewPrescriptionAsync(PrescriptionRequestDTO request)
    {
        if (request.DueDate < request.Date)
        {
            return false;
        }

        var patient = await _prescriptionRepository.GetPatientWithDetailsAsync(request.IdPatient) ?? new Patient
        {
            FirstName = request.PatientFirstName,
            LastName = request.PatientLastName,
            Birthdate = request.PatientBirthDate
        };

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            Patient = patient,
            IdDoctor = request.IdDoctor,
            PrescriptionMedicaments = new List<PrescriptionMedicament>()
        };

        foreach (var med in request.Medicaments)
        {
            if (!await _prescriptionRepository.MedicamentExistsAsync(med.IdMedicament))
            {
                return false;
            }
            
            prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdMedicament = med.IdMedicament,
                Dose = med.Dose,
                Details = med.Description
            });
        }
        
        try
        {
            await _prescriptionRepository.AddPrescriptionAsync(prescription);
        }
        catch (DbUpdateConcurrencyException e)
        {
            Console.WriteLine(e);
            return false;
        }
        return true;
    }


    public async Task<PatientDetailsDTO> GetPatientDetailsAsync(int patientId)
    {
        var patient = await _prescriptionRepository.GetPatientWithDetailsAsync(patientId);

        if (patient == null)
        {
            return null;
        }

        var patientDetails = new PatientDetailsDTO
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = patient.Prescriptions.OrderBy(pr => pr.DueDate)
                .Select(pr => new PrescriptionDetailsDTO
                {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    Medicaments = pr.PrescriptionMedicaments
                        .Select(pm => new MedicamentDetailsDTO
                        {
                            IdMedicament = pm.IdMedicament,
                            Name = pm.Medicament.Name,
                            Dose = pm.Dose,
                            Description = pm.Medicament.Description
                        }).ToList(),
                    Doctor = new DoctorDetailsDTO
                    {
                        IdDoctor = pr.Doctor.IdDoctor,
                        FirstName = pr.Doctor.FirstName,
                        LastName = pr.Doctor.LastName,
                        Email = pr.Doctor.Email
                    }
                }).ToList()
        };

        return patientDetails;
    }
}
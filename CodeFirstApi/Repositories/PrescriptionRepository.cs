using EntityFrameworkApi.Database;
using EntityFrameworkApi.Interfaces;
using EntityFrameworkApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkApi.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly PharmacyContext _context;

    public PrescriptionRepository(PharmacyContext context)
    {
        _context = context;
    }

    public async Task AddPrescriptionAsync(Prescription prescription)
    {
        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task<Patient> GetPatientWithDetailsAsync(int patientId)
    {
        return await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == patientId);
    }

    public async Task<bool> MedicamentExistsAsync(int medicamentId)
    {
        return await _context.Medicaments.AnyAsync(m => m.IdMedicament == medicamentId);
    }
}
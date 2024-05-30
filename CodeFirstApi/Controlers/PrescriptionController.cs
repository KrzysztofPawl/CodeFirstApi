using EntityFrameworkApi.DTO;
using EntityFrameworkApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkApi.Controlers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    
    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddNewPrescription([FromBody] PrescriptionRequestDTO request)
    {
        if (request == null || request.Medicaments == null || request.Medicaments.Count > 10)
        {
            return BadRequest("Invalid Request");
        }

        bool result = await _prescriptionService.AddNewPrescriptionAsync(request);

        if (!result)
        {
            return BadRequest("Error adding prescription");
        }

        return Ok("New Prescription added");
    }
    
    [HttpGet("{idPatient}")]
    public async Task<IActionResult> GetPatientDetails(int idPatient)
    {
        var patientDetails = await _prescriptionService.GetPatientDetailsAsync(idPatient);

        if (patientDetails == null)
        {
            return NotFound();
        }

        return Ok(patientDetails);
    }
}
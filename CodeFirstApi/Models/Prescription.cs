using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkApi.Models;

[Table("Prescription", Schema = "Pharmacy")]
public class Prescription
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdPrescription { get; set; }
    
    public DateTime Date { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public int IdPatient { get; set; }
    
    [ForeignKey("IdPatient")]
    public Patient Patient { get; set; }
    
    public int IdDoctor { get; set; }
    
    [ForeignKey("IdDoctor")]
    public Doctor Doctor { get; set; }

    public List<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new 
        List<PrescriptionMedicament>();
    
    [Timestamp]
    public byte[] RowVersion { get; set; }
}
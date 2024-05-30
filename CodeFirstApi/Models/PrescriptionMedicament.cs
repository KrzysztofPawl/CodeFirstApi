using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkApi.Models;

[Table("Prescription_Medicament", Schema = "Pharmacy")]
public class PrescriptionMedicament
{
    [Key, Column(Order = 0)]
    public int IdMedicament { get; set; }
    
    [Key, Column(Order = 1)]
    public int IdPrescription { get; set; }
    
    [ForeignKey("IdMedicament")]
    public Medicament Medicament { get; set; }
    
    [ForeignKey("IdPrescription")]
    public Prescription Prescription { get; set; }
    
    public int? Dose { get; set; }
    
    [MaxLength(100)]
    public string Details { get; set; }
}
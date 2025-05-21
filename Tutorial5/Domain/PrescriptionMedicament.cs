namespace Tutorial5.Domain;

public class PrescriptionMedicament
{
    public int PrescriptionId { get; set; }
    public Prescription Prescription { get; set; } = null!;

    public int MedicamentId { get; set; }
    public Medicament Medicament { get; set; } = null!;

    public int Dose { get; set; }
    public string Description { get; set; } = null!;
}
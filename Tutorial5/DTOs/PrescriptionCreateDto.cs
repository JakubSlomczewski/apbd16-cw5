namespace Tutorial5.DTOs;

public class PrescriptionCreateDto
{
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public PatientDto Patient { get; set; } = null!;
    public int DoctorId { get; set; }
    public List<MedicamentDto> Medicaments { get; set; } = new();
}

public class PatientDto
{
    public string FirstName { get; set; } = null!;
    public string LastName  { get; set; } = null!;
    public DateTime BirthDate { get; set; }
}

public class MedicamentDto
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; } = null!;
}
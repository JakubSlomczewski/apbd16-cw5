namespace Tutorial5.DTOs;

public class PatientDetailDto
{
    public int IdPatient  { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName  { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public List<PrescriptionDetailDto> Prescriptions { get; set; } = new();
}

public class PrescriptionDetailDto
{
    public int IdPrescription { get; set; }
    public DateTime Date    { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorDto Doctor { get; set; } = null!;
    public List<MedicamentDetailDto> Medicaments { get; set; } = new();
}

public class DoctorDto
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName  { get; set; } = null!;
}

public class MedicamentDetailDto
{
    public int IdMedicament { get; set; }
    public string Name      { get; set; } = null!;
    public int Dose         { get; set; }
    public string Description { get; set; } = null!;
}
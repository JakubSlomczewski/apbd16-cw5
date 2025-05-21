using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.DTOs;

namespace Tutorial5.Services;

public class PatientService : IPatientService
{
    private readonly DatabaseContext _db;
    public PatientService(DatabaseContext db) => _db = db;

    public async Task<PatientDetailDto> GetDetailsAsync(int idPatient)
    {
        var p = await _db.Patients
                    .Include(x => x.Prescriptions)
                    .ThenInclude(pr => pr.Doctor)
                    .Include(x => x.Prescriptions)
                    .ThenInclude(pr => pr.PrescriptionMedicaments)
                    .ThenInclude(pm => pm.Medicament)
                    .FirstOrDefaultAsync(x => x.IdPatient == idPatient)
                ?? throw new KeyNotFoundException("Pacjent nie istnieje.");

        return new PatientDetailDto {
            IdPatient   = p.IdPatient,
            FirstName   = p.FirstName,
            LastName    = p.LastName,
            BirthDate   = p.BirthDate,
            Prescriptions = p.Prescriptions
                .OrderBy(pr => pr.DueDate)
                .Select(pr => new PrescriptionDetailDto {
                    IdPrescription = pr.IdPrescription,
                    Date           = pr.Date,
                    DueDate        = pr.DueDate,
                    Doctor         = new DoctorDto {
                        IdDoctor  = pr.Doctor.IdDoctor,
                        FirstName = pr.Doctor.FirstName,
                        LastName  = pr.Doctor.LastName
                    },
                    Medicaments = pr.PrescriptionMedicaments
                        .Select(pm => new MedicamentDetailDto {
                            IdMedicament = pm.MedicamentId,
                            Name         = pm.Medicament.Name,
                            Dose         = pm.Dose,
                            Description  = pm.Description
                        }).ToList()
                }).ToList()
        };
    }
}
using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.DTOs;
using Tutorial5.Domain;

namespace Tutorial5.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly DatabaseContext _db;
    public PrescriptionService(DatabaseContext db) => _db = db;

    public async Task AddAsync(PrescriptionCreateDto dto)
    {
        if (dto.Medicaments.Count > 10)
            throw new ArgumentException("Recepta może mieć maks. 10 leków."); 
        if (dto.DueDate < dto.Date)
            throw new ArgumentException("DueDate musi być >= Date.");

        var doctor = await _db.Doctors.FindAsync(dto.DoctorId)
                     ?? throw new KeyNotFoundException("Lekarz nie istnieje.");

        var patient = await _db.Patients
                          .FirstOrDefaultAsync(p =>
                              p.FirstName == dto.Patient.FirstName &&
                              p.LastName  == dto.Patient.LastName &&
                              p.BirthDate == dto.Patient.BirthDate)
                      ?? new Patient {
                          FirstName = dto.Patient.FirstName,
                          LastName  = dto.Patient.LastName,
                          BirthDate = dto.Patient.BirthDate
                      };
        if (patient.IdPatient == 0) _db.Patients.Add(patient);

        var pres = new Prescription {
            Date      = dto.Date,
            DueDate   = dto.DueDate,
            Doctor    = doctor,
            Patient   = patient
        };

        foreach (var m in dto.Medicaments)
        {
            var med = await _db.Medicaments.FindAsync(m.IdMedicament)
                      ?? throw new KeyNotFoundException($"Lek id={m.IdMedicament} nie istnieje.");

            pres.PrescriptionMedicaments.Add(new PrescriptionMedicament {
                Medicament = med,
                Dose       = m.Dose,
                Description= m.Description
            });
        }

        _db.Prescriptions.Add(pres);
        await _db.SaveChangesAsync();
    }
}
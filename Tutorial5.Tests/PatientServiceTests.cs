using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.Domain;
using Tutorial5.DTOs;
using Tutorial5.Services;
using Xunit;

namespace Tutorial5.Tests
{
    public class PatientServiceTests
    {
        private DatabaseContext GetContext(string name) =>
            new(new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(name)
                .Options);

        [Fact]
        public async Task GetDetailsAsync_PatientExists_ReturnsDto()
        {
            
            var ctx = GetContext("exists");
            
            var doc = new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "K", Email = "a@b" };
            var med = new Medicament { IdMedicament = 1, Name = "Test" };
            var pat = new Patient { IdPatient = 1, FirstName = "Anna", LastName = "Nowak", BirthDate = new DateTime(1985,9,15) };
            var pres = new Prescription {
                IdPrescription = 1,
                Date = DateTime.Today,
                DueDate = DateTime.Today.AddDays(1),
                Doctor = doc,
                Patient = pat
            };
            pres.PrescriptionMedicaments.Add(new PrescriptionMedicament {
                Medicament = med, Dose = 1, Description = "Desc"
            });
            ctx.AddRange(doc, med, pat, pres);
            await ctx.SaveChangesAsync();

            var service = new PatientService(ctx);

          
            var dto = await service.GetDetailsAsync(1);

        
            Assert.Equal("Anna", dto.FirstName);
            Assert.Single(dto.Prescriptions);
            var presDto = dto.Prescriptions.Single();
            Assert.Equal(1, presDto.Medicaments.Count);
        }

        [Fact]
        public async Task GetDetailsAsync_PatientNotExists_ThrowsKeyNotFound()
        {
            var ctx = GetContext("notexists");
            var service = new PatientService(ctx);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetDetailsAsync(999));
        }
    }
}

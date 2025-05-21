using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.Domain;
using Tutorial5.DTOs;
using Tutorial5.Services;
using Xunit;

namespace Tutorial5.Tests
{
    public class PrescriptionServiceTests
    {
        private DatabaseContext GetContext(string name) =>
            new(new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(name)
                .Options);

        [Fact]
        public async Task AddAsync_ValidData_CreatesPrescriptionAndPatient()
        {
          
            var ctx = GetContext("valid");
            ctx.Doctors.Add(new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jk@e.pl" });
            ctx.Medicaments.Add(new Medicament { IdMedicament = 1, Name = "Apap" });
            await ctx.SaveChangesAsync();

            var service = new PrescriptionService(ctx);
            var dto = new PrescriptionCreateDto {
                Date = DateTime.Today,
                DueDate = DateTime.Today.AddDays(1),
                DoctorId = 1,
                Patient = new PatientDto {
                    FirstName = "Ala", LastName = "Kowalska", BirthDate = new DateTime(1995,1,1)
                },
                Medicaments = new List<MedicamentDto> {
                    new() { IdMedicament = 1, Dose = 2, Description = "Ok" }
                }
            };

           
            await service.AddAsync(dto);

           
            Assert.Single(ctx.Patients);
            Assert.Single(ctx.Prescriptions);
        }

        [Fact]
        public async Task AddAsync_DoctorNotExists_ThrowsKeyNotFound()
        {
            var ctx = GetContext("noDr");
            var service = new PrescriptionService(ctx);
            var dto = new PrescriptionCreateDto {
                Date = DateTime.Today,
                DueDate = DateTime.Today,
                DoctorId = 42,
                Patient = new PatientDto { FirstName = "X", LastName = "Y", BirthDate = DateTime.Today },
                Medicaments = new List<MedicamentDto>()
            };

            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.AddAsync(dto));
        }
        
    }
}

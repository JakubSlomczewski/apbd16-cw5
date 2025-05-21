using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IPatientService, PatientService>();


builder.Services.AddControllers();


var app = builder.Build();

app.UseRouting();


app.MapControllers();

app.Run();
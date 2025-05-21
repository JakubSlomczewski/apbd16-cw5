using Microsoft.AspNetCore.Mvc;
using Tutorial5.Services;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _svc;
    public PatientsController(IPatientService svc) => _svc = svc;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try {
            var dto = await _svc.GetDetailsAsync(id);
            return Ok(dto);
        }
        catch (KeyNotFoundException ex) {
            return NotFound(ex.Message);
        }
    }
}
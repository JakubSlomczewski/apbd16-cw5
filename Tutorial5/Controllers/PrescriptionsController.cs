using Microsoft.AspNetCore.Mvc;
using Tutorial5.DTOs;
using Tutorial5.Services;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _svc;
    public PrescriptionsController(IPrescriptionService svc) => _svc = svc;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PrescriptionCreateDto dto)
    {
        try {
            await _svc.AddAsync(dto);
            return CreatedAtAction(null, null);
        }
        catch (ArgumentException ex) {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex) {
            return NotFound(ex.Message);
        }
    }
}
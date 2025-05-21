using Tutorial5.DTOs;

namespace Tutorial5.Services;

public interface IPrescriptionService
{
    Task AddAsync(PrescriptionCreateDto dto);
}
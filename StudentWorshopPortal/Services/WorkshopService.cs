using StudentWorkshopPortal.Repositories.Interfaces;
using StudentWorshopPortal.Models;

namespace StudentWorshopPortal.Services;

public class WorkshopService
{
    private readonly IWorkshopRepository _workshopRepository;

    public WorkshopService(IWorkshopRepository workshopRepository)
    {
        _workshopRepository = workshopRepository;
    }

    public async Task<IEnumerable<Workshop>> GetWorkshopsAsync()
    {
        return await _workshopRepository.GetAllAsync();
    }

    public async Task<Workshop?> GetWorkshopAsync(int id)
    {
        return await _workshopRepository.GetByIdAsync(id);
    }

    public async Task SaveWorkshopAsync(Workshop workshop)
    {
        ArgumentNullException.ThrowIfNull(workshop);

        await _workshopRepository.AddAsync(workshop);

        await _workshopRepository.SaveChangesAsync();
    }

    public async Task UpdateWorkshopAsync(Workshop workshop)
    {
        ArgumentNullException.ThrowIfNull(workshop);

        if (!await _workshopRepository.ExistsAsync(workshop.Id))
        {
            throw new InvalidOperationException(
                $"Workshop {workshop.Id} was not found.");
        }

        await _workshopRepository.UpdateAsync(workshop);

        await _workshopRepository.SaveChangesAsync();
    }

    public async Task DeleteWorkshopAsync(int id)
    {
        var workshop = await _workshopRepository.GetByIdAsync(id);

        if (workshop == null)
        {
            throw new InvalidOperationException(
                $"Workshop {id} was not found.");
        }

        if (workshop.Registrations.Any())
        {
            throw new InvalidOperationException(
                "Cannot delete a workshop with existing registrations.");
        }

        await _workshopRepository.DeleteAsync(id);

        await _workshopRepository.SaveChangesAsync();
    }
}
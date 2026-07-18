using StudentWorshopPortal.Models;

namespace StudentWorkshopPortal.Repositories.Interfaces;

public interface IRegistrationRepository
{
    Task<IEnumerable<Registration>> GetAllAsync();

    Task<Registration?> GetByIdAsync(int id);

    Task<IEnumerable<Registration>> GetByWorkshopAsync(int workshopId);

    Task AddAsync(Registration registration);

    Task UpdateAsync(Registration registration);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);

    Task<bool> ExistsAsync(string studentNumber, int workshopId);

    Task SaveChangesAsync();
}
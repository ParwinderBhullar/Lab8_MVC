using StudentWorshopPortal.Models;

namespace StudentWorkshopPortal.Repositories.Interfaces;

public interface IWorkshopRepository
{
    Task<IEnumerable<Workshop>> GetAllAsync();

    Task<Workshop?> GetByIdAsync(int id);

    Task AddAsync(Workshop workshop);

    Task UpdateAsync(Workshop workshop);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);

    Task SaveChangesAsync();
}
using Microsoft.EntityFrameworkCore;
using StudentWorkshopPortal.Data;
using StudentWorkshopPortal.Repositories.Interfaces;
using StudentWorshopPortal.Models;

namespace StudentWorkshopPortal.Repositories;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly ApplicationDbContext _context;

    public RegistrationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Registration>> GetAllAsync()
    {
        return await _context.Registrations
            .Include(r => r.Workshop)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Registration?> GetByIdAsync(int id)
    {
        return await _context.Registrations
            .Include(r => r.Workshop)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Registration>> GetByWorkshopAsync(int workshopId)
    {
        return await _context.Registrations
            .Where(r => r.WorkshopId == workshopId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Registration registration)
    {
        await _context.Registrations.AddAsync(registration);
    }

    public Task UpdateAsync(Registration registration)
    {
        _context.Registrations.Update(registration);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var registration = await GetByIdAsync(id);

        if (registration != null)
        {
            _context.Registrations.Remove(registration);
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Registrations
            .AnyAsync(r => r.Id == id);
    }

    public async Task<bool> ExistsAsync(string studentNumber, int workshopId)
    {
        return await _context.Registrations.AnyAsync(r =>
            r.StudentNumber == studentNumber &&
            r.WorkshopId == workshopId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
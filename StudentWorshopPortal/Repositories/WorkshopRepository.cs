using Microsoft.EntityFrameworkCore;
using StudentWorkshopPortal.Data;
using StudentWorkshopPortal.Repositories.Interfaces;
using StudentWorshopPortal.Models;

namespace StudentWorkshopPortal.Repositories;

public class WorkshopRepository : IWorkshopRepository
{
    private readonly ApplicationDbContext _context;

    public WorkshopRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Workshop>> GetAllAsync()
    {
        return await _context.Workshops
            .AsNoTracking()
            .OrderBy(w => w.Date)
            .ToListAsync();
    }

    public async Task<Workshop?> GetByIdAsync(int id)
    {
        return await _context.Workshops
            .Include(w => w.Registrations)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task AddAsync(Workshop workshop)
    {
        await _context.Workshops.AddAsync(workshop);
    }

    public Task UpdateAsync(Workshop workshop)
    {
        _context.Workshops.Update(workshop);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var workshop = await GetByIdAsync(id);

        if (workshop != null)
        {
            _context.Workshops.Remove(workshop);
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Workshops
            .AnyAsync(w => w.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
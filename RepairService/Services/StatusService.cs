using Microsoft.EntityFrameworkCore;
using RepairService.Contexts;
using RepairService.Models.Entities;

namespace RepairService.Services;

internal class StatusService
{
    private readonly DataContext _context = new();

    public async Task StartAsync()
    {
        if (!await _context.Statuses.AnyAsync())
        {
            var statuslist = new List<StatusEntity>()
            {
                new StatusEntity() { RepairStatus = "Ej påbörjad" },
                new StatusEntity() { RepairStatus = "Pågående" },
                new StatusEntity() { RepairStatus = "Avslutad "},
            };

            _context.AddRange(statuslist);
            await _context.SaveChangesAsync();  
        }
    }

    public async Task<IEnumerable<StatusEntity>> GetAllAsync()
    {
        return await _context.Statuses.ToListAsync();
    }
}


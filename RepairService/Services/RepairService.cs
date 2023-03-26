using Microsoft.EntityFrameworkCore;
using RepairService.Contexts;
using RepairService.Models.Entities;
using System.Linq.Expressions;

namespace RepairService.Services;

internal class RepairService
{
    private readonly DataContext _context = new DataContext();
    private readonly TennantService _tennantService = new TennantService();
    private readonly StatusService _statusService = new StatusService();

    public async Task CreateAsync(RepairEntity repairEntity)
    {
        if (await _tennantService.GetAsync(tennantEntity => tennantEntity.Id == repairEntity.TennantId) != null &&
            await _statusService.GetAsync(statusEntity => statusEntity.Id == repairEntity.StatusId) != null)
        {
            _context.Add(repairEntity);
            await _context.SaveChangesAsync();
        }
  
    }

    public async Task<IEnumerable<RepairEntity>> GetAllAsync()
    {
        return await _context.Repairs
            .Include(x => x.Tennant)
            .Include(x => x.Status)
            .Include(x => x.Comments)
            .ToListAsync();
    }

    public async Task<RepairEntity> GetAsync(Expression<Func<RepairEntity, bool>> predicate)
    {
        var _repairEntity = await _context.Repairs
            .Include(x => x.Tennant)
            .Include(x => x.Status)
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(predicate);
        return _repairEntity!;
    }

    public async Task <RepairEntity> UpdateRepairStatusAsync(Expression<Func<RepairEntity, bool>> predicate)
    {
        var _repairEntity = await _context.Repairs.FirstOrDefaultAsync(predicate);
        if(_repairEntity != null)
        {
            if (_repairEntity.StatusId == 1)
            {
                _repairEntity.StatusId = 2;
            } else if (_repairEntity.StatusId == 2)
            {
                _repairEntity.StatusId = 3;
            } else
            {
                _repairEntity.StatusId = 2;
            }
            _context.Update(_repairEntity);
            await _context.SaveChangesAsync();
        }

        return _repairEntity!;
    }

}

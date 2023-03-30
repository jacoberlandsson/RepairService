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

    
    public async Task<RepairEntity> CreateAsync(RepairEntity repairEntity)
    {
        var _tennant = await _tennantService.GetAsync(tennantEntity => tennantEntity.Id == repairEntity.TennantId);
        var _status = await _statusService.GetAsync(statusEntity => statusEntity.Id == repairEntity.StatusId);
        if (_tennant != null && _status != null)
        {
            
            _context.Repairs.Add(repairEntity);
            await _context.SaveChangesAsync();

        }
        return repairEntity!;
        
    }
    
    

    public async Task<IEnumerable<RepairEntity>> GetAllRepairsAsync()
    {
        return await _context.Repairs
            .Include(x => x.Tennant)
            .Include(x => x.Status)
            .Include(x => x.Comments)
            .ToListAsync();
        
    }

    public async Task<IEnumerable<RepairEntity>> GetAllActiveRepairsAsync()
    {
        return await _context.Repairs
            .Include(x => x.Tennant)
            .Include(x => x.Status)
            .Include(x => x.Comments)
            .Where(x => x.StatusId != 3)
            .ToListAsync();
           
    }

    public async Task<RepairEntity> GetAsync(Expression<Func<RepairEntity, bool>> predicate)
    {
        var _repairEntity = await _context.Repairs
            .Include(x => x.Tennant)
            .Include(x => x.Description)
            .Include(x => x.Status)
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(predicate);
        return _repairEntity!;
    }

    public async Task<IEnumerable<RepairEntity>> GetSpecificRepairsAsync(string name)
    {
        return await _context.Repairs
            .Include(x => x.Tennant)
            .Include(x => x.Status)
            .Include(x => x.Comments)
            .Where(x => x.Tennant.TennantName == name)
            .ToListAsync();

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

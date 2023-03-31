using Microsoft.EntityFrameworkCore;
using RepairService.Contexts;
using RepairService.Models.Entities;
using System.Linq.Expressions;

namespace RepairService.Services;

internal class TennantService
{
    private readonly DataContext _context = new();

    public async Task<TennantEntity> CreateAsync(TennantEntity tennantEntity)
    {
        var _tennantEntity = await GetAsync(x => x.TennantEmail == tennantEntity.TennantEmail);
        if (_tennantEntity == null)
        {
            _tennantEntity = tennantEntity;
            _context.Add(_tennantEntity);
            await _context.SaveChangesAsync();
        }
        return _tennantEntity;
    }

    public async Task<TennantEntity> GetAsync(Expression<Func<TennantEntity, bool>> predicate)
    {
        var _tennantEntity = await _context.Tennants.FirstOrDefaultAsync(predicate);
        return _tennantEntity!;
    }
}

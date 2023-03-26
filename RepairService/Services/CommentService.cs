using RepairService.Contexts;
using RepairService.Models.Entities;

namespace RepairService.Services;

internal class CommentService
{
    private readonly DataContext _context = new DataContext();

    public async Task CreateAsync(CommentEntity commentEntity)
    {
        if (await _context.Repairs.AnyAsync(x => x.Id == commentEntity.RepairId))
        {
            _context.Add(commentEntity);
            await _context.SaveChangesAsync();
        }

    }
    public async Task<IEnumerable<CommentEntity>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }

}

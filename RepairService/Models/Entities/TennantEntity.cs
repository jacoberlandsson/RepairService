namespace RepairService.Models.Entities;

internal class TennantEntity
{
    public int Id { get; set; }
    public string TennantName { get; set; } = null!;

    public string TennantEmail { get; set; } = null!;
    
    public ICollection<RepairEntity> Repairs { get; set; } = new HashSet<RepairEntity>();

}

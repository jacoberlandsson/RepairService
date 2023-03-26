namespace RepairService.Models.Entities;

internal class StatusEntity
{
    public int Id { get; set; }
    public string RepairStatus { get; set; } = null!;
    public ICollection<RepairEntity> Repairs { get; set; } = new HashSet<RepairEntity>();

}

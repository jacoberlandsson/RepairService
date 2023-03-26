namespace RepairService.Models.Entities;

internal class CommentEntity
{
    public int Id { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.Now;
    public int RepairId { get; set; }
    public RepairEntity Repair { get; set; } = null!;
}

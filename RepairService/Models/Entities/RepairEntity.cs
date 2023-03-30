namespace RepairService.Models.Entities;

internal class RepairEntity
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastUpdated { get; set; } = DateTime.Now;

    public int StatusId { get; set; } = 1;
    public StatusEntity Status { get; set; } = null!;

    public int TennantId { get; set; }
    public TennantEntity Tennant { get; set; } = null!;

    public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();

}

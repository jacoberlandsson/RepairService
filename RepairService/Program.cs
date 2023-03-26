using RepairService.Services;

var statusService = new StatusService();

await statusService.StartAsync();

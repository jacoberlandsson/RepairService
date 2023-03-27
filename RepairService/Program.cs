using RepairService.Services;

var statusService = new StatusService();
var menuService = new MenuService();

await statusService.StartAsync();

while(true)
{
    Console.Clear();
    await menuService.MainMenu();
    Console.ReadKey();
}
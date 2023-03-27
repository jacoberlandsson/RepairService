using Microsoft.EntityFrameworkCore.Metadata;
using RepairService.Models.Entities;

namespace RepairService.Services;

internal class MenuService
{
   private readonly RepairService _repairService = new RepairService(); 
   private readonly TennantService _tennantService = new TennantService();

    public async Task MainMenu()
    {
        Console.Clear();
        Console.WriteLine("||||||||||| Välkommen till Reparationsservice, var god välj ett alternativ nedan |||||||||||");
        Console.WriteLine("1. Visa alla aktiva reparationer");
        Console.WriteLine("2. Visa alla reparationer");
        Console.WriteLine("3. Ny felanmälan");

        var menuOption = Console.ReadLine();

       switch(menuOption)
        {
            case "1":
                await ActiveRepairsAsync();
                break;
            case "2":
                await AllRepairsAsync();
                break;
            case "3":
                await NewRepairAsync(); 
                break;
            default:
                Console.Clear();
                Console.WriteLine("Ogiltigt menyval");
                break;
        }
    }

    private async Task ActiveRepairsAsync()
    {
        Console.Clear();
        Console.WriteLine("||||||||||| Pågående reparationer |||||||||||");
        foreach(var _repairs in await _repairService.GetAllActiveRepairsAsync())
        {
            Console.WriteLine($"Ärendenummer:  {_repairs.Id}");
            Console.WriteLine($"Hyresgäst:  {_repairs.Tennant}");
            Console.WriteLine($"Datum för felanmälan:  {_repairs.Created}");
            Console.WriteLine($"Ärendets beskrivning:  {_repairs.Description}");
            Console.WriteLine($"Kommentarer:  {_repairs.Comments}");
            Console.WriteLine($"Status:  {_repairs.Status.RepairStatus}");
            Console.WriteLine("");
  

        }
    }

    private async Task AllRepairsAsync()
    {
        Console.Clear();
        Console.WriteLine("||||||||||| Pågående reparationer |||||||||||");
        foreach (var _repairs in await _repairService.GetAllRepairsAsync())
        {
            Console.WriteLine($"Ärendenummer:  {_repairs.Id}");
            Console.WriteLine($"Hyresgäst:  {_repairs.Tennant}");
            Console.WriteLine($"Datum för felanmälan:  {_repairs.Created}");
            Console.WriteLine($"Ärendets beskrivning:  {_repairs.Description}");
            Console.WriteLine($"Kommentarer:  {_repairs.Comments}");
            Console.WriteLine($"Status:  {_repairs.Status.RepairStatus}");
            Console.WriteLine("");


        }
    }

    private async Task NewRepairAsync()
    {
        Console.Clear();
        var _entity = new RepairEntity();
        var _tennantEntity = new TennantEntity();

        Console.WriteLine("Hyresgästens namn: ");
        _tennantEntity.TennantName = Console.ReadLine() ?? "";
        Console.WriteLine("Hyresgästens email: ");
        _tennantEntity.TennantEmail = Console.ReadLine() ?? "";
        Console.WriteLine("Ärendebeskrivning - beskriv vad som behöver åtgärdas: ");
        _entity.Description = Console.ReadLine() ?? "";

        await _tennantService.CreateAsync( _tennantEntity );
        await _repairService.CreateAsync(_entity);

        await ActiveRepairsAsync();

    }
}   


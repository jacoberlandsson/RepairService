using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
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
            Console.WriteLine($"Hyresgäst:  {_repairs.Tennant.TennantName}");
            Console.WriteLine($"Datum för felanmälan:  {_repairs.Created}");
            Console.WriteLine($"Ärendets beskrivning:  {_repairs.Description}");
            Console.WriteLine($"Kommentarer: ");
            foreach (var comment in _repairs.Comments)
            {
                Console.WriteLine(comment.Comment);
            }
            Console.WriteLine($"Status:  {_repairs.Status.RepairStatus}");
            Console.WriteLine("");


        }
    }

    private async Task AllRepairsAsync()
    {
        Console.Clear();
        Console.WriteLine("||||||||||| Alla reparationer |||||||||||");
        foreach (var _repairs in await _repairService.GetAllRepairsAsync())
        {
            Console.WriteLine($"Ärendenummer:  {_repairs.Id}");
            Console.WriteLine($"Hyresgäst:  {_repairs.Tennant.TennantName}");
            Console.WriteLine($"Datum för felanmälan:  {_repairs.Created}");
            Console.WriteLine($"Ärendets beskrivning:  {_repairs.Description}");
            Console.WriteLine($"Kommentarer: ");
            foreach (var comment in _repairs.Comments)
            {
                Console.WriteLine(comment.Comment);
            }
            Console.WriteLine($"Status:  {_repairs.Status.RepairStatus}");
            Console.WriteLine("");


        }
    }

    private async Task NewRepairAsync()
    {
        Console.Clear();
        var _tennantEntity = new TennantEntity();
        var _entity = new RepairEntity();

        Console.WriteLine("Ny felanmälan: ");
        Console.WriteLine("Hyresgästens namn: ");
        _tennantEntity.TennantName = Console.ReadLine() ?? "";
        Console.WriteLine("Hyresgästens email: ");
        _tennantEntity.TennantEmail = Console.ReadLine() ?? "";
        Console.WriteLine("Ärendebeskrivning - beskriv vad som behöver åtgärdas: ");
        _entity.Description = Console.ReadLine() ?? "";

        _tennantEntity = await _tennantService.CreateAsync( _tennantEntity );
        _entity.TennantId = _tennantEntity.Id;
        await _repairService.CreateAsync(_entity);

        

    }
}   


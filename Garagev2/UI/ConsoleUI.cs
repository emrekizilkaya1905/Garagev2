using Garagev2.Models;
using Garagev2.Services;
using static Garagev2.UI.ConsoleInput;
using static Garagev2.UI.GarageMenuActions;

namespace Garagev2.UI;

public class ConsoleUI
{
    public void Run()
    {
        try
        {
            Console.WriteLine("=== Garage 1.0 ===");
            Console.WriteLine("Create your garage to get started. Use /cancel to exit.");
            var garage = new Garage(ReadInteger("Capacity", 1));
            var actions = new GarageMenuActions(garage);
            Console.WriteLine($"Garage created with {garage.Capacity} spaces.");

            while (true)
            {
                Console.WriteLine("\n=== Garage menu ===");
                Console.WriteLine($"Spaces used: {garage.Count}/{garage.Capacity}");
                Console.WriteLine("1. List vehicles\n2. List vehicle type counts\n3. Park vehicle\n4. Remove vehicle\n5. Find by registration number\n6. Search vehicles\n7. Add sample vehicles\n0. Exit");
                Console.Write("Select an option: ");
                string choice = ReadLine();
                if (choice == "0") break;

                try
                {
                    switch (choice)
                    {
                        case "1": DisplayVehicles(garage.GetAllVehicles()); break;
                        case "2": actions.DisplayTypeCounts(); break;
                        case "3": actions.ParkVehicle(); break;
                        case "4": actions.RemoveVehicle(); break;
                        case "5":
                            Vehicle? vehicle = garage.FindByRegistrationNumber(ReadText("Registration number"));
                            Console.WriteLine(vehicle?.ToString() ?? "No matching vehicle found.");
                            break;
                        case "6": actions.SearchVehicles(); break;
                        case "7": actions.AddSampleVehicles(); break;
                        default: Console.WriteLine("Invalid option. Please try again."); break;
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Operation cancelled.");
                }
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Garage creation cancelled.");
        }
        catch (EndOfStreamException)
        {
            Console.WriteLine("\nInput closed.");
        }

        Console.WriteLine("Goodbye!");
    }
}



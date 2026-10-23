
using Garagev2.Models;
using static Garagev2.UI.ConsoleInput;

namespace Garagev2.UI;

public static class VehicleSearchForm
{
    public static VehicleSearchCriteria? ReadCriteria()
    {
        Console.WriteLine("\n=== Search vehicles ===");
        Console.WriteLine("1. By registration number\n2. By color\n3. By number of wheels\n4. By vehicle type\n5. By multiple criteria\n0. Back");
        while (true)
        {
            string choice = ReadText("Search option");
            switch (choice)
            {
                case "0": return null;
                case "1": return new VehicleSearchCriteria { RegistrationNumber = ReadText("Registration number") };
                case "2": return new VehicleSearchCriteria { Color = ReadText("Color") };
                case "3": return new VehicleSearchCriteria { NumberOfWheels = ReadInteger("Number of wheels", 0) };
                case "4": return new VehicleSearchCriteria { VehicleType = GetVehicleType(ReadVehicleType(false)) };
                case "5": return ReadMultipleCriteria();
                default: Console.WriteLine("Select an option from 0 to 5."); break;
            }
        }
    }

    private static VehicleSearchCriteria ReadMultipleCriteria()
    {
        Console.WriteLine("Leave a field blank and press Enter to skip it.");
        Console.WriteLine("All selected filters must match. Leave all fields blank to show all vehicles.");
        string registration = ReadText("Registration number", true);
        string color = ReadText("Color", true);
        int? wheels = ReadOptionalWheels();
        Type? type = GetVehicleType(ReadVehicleType(true));
        return new VehicleSearchCriteria
        {
            RegistrationNumber = registration,
            Color = color,
            NumberOfWheels = wheels,
            VehicleType = type
        };
    }

    private static int? ReadOptionalWheels()
    {
        while (true)
        {
            string input = ReadText("Number of wheels", true);
            if (input.Length == 0) return null;
            if (int.TryParse(input, out int wheels) && wheels >= 0) return wheels;
            Console.WriteLine("Enter a whole number of zero or greater, or leave blank.");
        }
    }

    private static Type? GetVehicleType(int type)
    {
        return type switch
        {
            1 => typeof(Motorcycle),
            2 => typeof(Airplane),
            3 => typeof(Car),
            4 => typeof(Bus),
            5 => typeof(Boat),
            _ => null
        };
    }
}


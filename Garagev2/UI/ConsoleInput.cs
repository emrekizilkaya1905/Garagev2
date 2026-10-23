using System.Globalization;
using Garagev2.Models;

namespace Garagev2.UI;

public static class ConsoleInput
{
    public static string ReadRegistrationNumber()
    {
        while (true)
        {
            string registration = ReadText("Registration number (ABC123 or ABC12A)");
            if (Vehicle.IsValidRegistrationNumber(registration)) return registration;
            Console.WriteLine("Invalid registration number. Use three letters, two digits, and a final letter or digit (ABC123 or ABC12A).");
        }
    }

    public static int ReadVehicleType(bool optional)
    {
        Console.WriteLine("1. Motorcycle\n2. Airplane\n3. Car\n4. Bus\n5. Boat");
        while (true)
        {
            string input = ReadText(optional ? "Vehicle type (blank for any)" : "Vehicle type", optional);
            if (optional && input.Length == 0) return 0;
            if (int.TryParse(input, out int type) && type >= 1 && type <= 5) return type;
            Console.WriteLine("Select a vehicle type from 1 to 5.");
        }
    }

    public static FuelType ReadFuelType()
    {
        Console.WriteLine("1. Gasoline\n2. Diesel");
        while (true)
        {
            string input = ReadText("Fuel type");
            if (input == "1") return FuelType.Gasoline;
            if (input == "2") return FuelType.Diesel;
            Console.WriteLine("Select 1 or 2.");
        }
    }

    public static double ReadLength()
    {
        while (true)
        {
            string input = ReadText("Length in meters (use a decimal point)");
            if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double length)
                && double.IsFinite(length) && length > 0) return length;
            Console.WriteLine("Enter a finite number greater than zero.");
        }
    }

    public static int ReadInteger(string prompt, int minimum)
    {
        while (true)
        {
            string input = ReadText(prompt);
            if (int.TryParse(input, out int value) && value >= minimum) return value;
            Console.WriteLine($"Enter a whole number from {minimum} to {int.MaxValue}.");
        }
    }

    public static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            string input = ReadText($"{prompt} (y/n)");
            if (input.Equals("y", StringComparison.OrdinalIgnoreCase)) return true;
            if (input.Equals("n", StringComparison.OrdinalIgnoreCase)) return false;
            Console.WriteLine("Enter y or n.");
        }
    }

    public static string ReadText(string prompt, bool allowEmpty = false)
    {
        while (true)
        {
            Console.Write($"{prompt} (/cancel to return): ");
            string input = ReadLine();
            if (input.Equals("/cancel", StringComparison.OrdinalIgnoreCase)) throw new OperationCanceledException();
            if (allowEmpty || input.Length > 0) return input;
            Console.WriteLine("This value cannot be empty.");
        }
    }

    public static string ReadLine()
    {
        return Console.ReadLine()?.Trim() ?? throw new EndOfStreamException();
    }
}




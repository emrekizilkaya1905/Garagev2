using Garagev2.Models;
using Garagev2.Services;
using static Garagev2.UI.ConsoleInput;

namespace Garagev2.UI;

public class GarageMenuActions
{
    private readonly Garage garage;

    public GarageMenuActions(Garage garage)
    {
        ArgumentNullException.ThrowIfNull(garage);
        this.garage = garage;
    }

    public void ParkVehicle()
    {
        if (garage.Count == garage.Capacity)
        {
            Console.WriteLine("The garage is full.");
            return;
        }

        Console.WriteLine(garage.Park(VehicleForm.ReadVehicle()).Message);
    }

    public void RemoveVehicle()
    {
        List<Vehicle> vehicles = garage.GetAllVehicles();
        if (vehicles.Count == 0)
        {
            Console.WriteLine("No vehicles parked.");
            return;
        }

        Console.WriteLine("\n=== Remove vehicle ===");
        for (int i = 0; i < vehicles.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {vehicles[i]}");
        }
        Console.WriteLine("0. Back");

        while (true)
        {
            int selection = ReadInteger("Select a vehicle number", 0);
            if (selection == 0) return;
            if (selection > vehicles.Count)
            {
                Console.WriteLine($"Select a vehicle from 1 to {vehicles.Count}, or 0 to go back.");
                continue;
            }

            Vehicle vehicle = vehicles[selection - 1];
            OperationResult result = garage.Remove(vehicle.RegistrationNumber);
            Console.WriteLine($"{vehicle.RegistrationNumber}: {result.Message}");
            return;
        }
    }

    public void SearchVehicles()
    {
        VehicleSearchCriteria? criteria = VehicleSearchForm.ReadCriteria();
        if (criteria is not null) DisplayVehicles(garage.Search(criteria));
    }

    public void DisplayTypeCounts()
    {
        var counts = garage.GetVehicleTypeCounts();
        if (counts.Count == 0) Console.WriteLine("No vehicles parked.");
        foreach (var entry in counts) Console.WriteLine($"{entry.Key}: {entry.Value}");
    }

    public static void DisplayVehicles(List<Vehicle> vehicles)
    {
        if (vehicles.Count == 0) Console.WriteLine("No matching vehicles found.");
        foreach (Vehicle vehicle in vehicles) Console.WriteLine(vehicle);
    }

    public void AddSampleVehicles()
    {
        List<Vehicle> samples =
        [
            new Motorcycle("MCA001", "Pink", 3, 600),
            new Airplane("AIR001", "White", 3, 2),
            new Car("ABC123", "Black", 4, FuelType.Gasoline),
            new Bus("BUS001", "Red", 6, 40),
            new Boat("BOA001", "Blue", 0, 8.5)
        ];
        foreach (Vehicle vehicle in samples)
        {
            Console.WriteLine($"{vehicle.RegistrationNumber}: {garage.Park(vehicle).Message}");
        }
    }

}








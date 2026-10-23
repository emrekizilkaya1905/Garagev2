using Garagev2.Models;

namespace Garagev2.Services;

public class Garage
{
    private readonly List<Vehicle> vehicles = new();

    public int Capacity { get; }
    public int Count => vehicles.Count;

    public Garage(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");
        }

        Capacity = capacity;
    }

    public OperationResult Park(Vehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);

        if (FindByRegistrationNumber(vehicle.RegistrationNumber) is not null)
        {
            return new OperationResult(false, "A vehicle with this registration number is already parked.");
        }

        if (Count >= Capacity)
        {
            return new OperationResult(false, "The garage is full.");
        }

        vehicles.Add(vehicle);
        return new OperationResult(true, "Vehicle parked successfully.");
    }

    public OperationResult Remove(string registrationNumber)
    {
        if (string.IsNullOrWhiteSpace(registrationNumber))
        {
            return new OperationResult(false, "Registration number cannot be empty.");
        }

        Vehicle? vehicle = FindByRegistrationNumber(registrationNumber);
        if (vehicle is null)
        {
            return new OperationResult(false, "No vehicle with this registration number was found.");
        }

        vehicles.Remove(vehicle);
        return new OperationResult(true, "Vehicle removed successfully.");
    }

    public Vehicle? FindByRegistrationNumber(string registrationNumber)
    {
        if (string.IsNullOrWhiteSpace(registrationNumber))
        {
            return null;
        }

        return vehicles.FirstOrDefault(vehicle =>
            string.Equals(vehicle.RegistrationNumber, registrationNumber.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    public List<Vehicle> GetAllVehicles()
    {
        return vehicles.ToList();
    }

    public Dictionary<string, int> GetVehicleTypeCounts()
    {
        return GetAllVehicles()
            .GroupBy(vehicle => vehicle.GetType().Name)
            .ToDictionary(group => group.Key, group => group.Count());
    }


    public List<Vehicle> Search(VehicleSearchCriteria criteria)
    {
        ArgumentNullException.ThrowIfNull(criteria);

        if (criteria.NumberOfWheels < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(criteria), "Number of wheels cannot be negative.");
        }

        if (criteria.VehicleType is not null && !typeof(Vehicle).IsAssignableFrom(criteria.VehicleType))
        {
            throw new ArgumentException("Vehicle type must inherit from Vehicle.", nameof(criteria));
        }

        return GetAllVehicles().Where(vehicle =>
            (string.IsNullOrWhiteSpace(criteria.RegistrationNumber) ||
                string.Equals(vehicle.RegistrationNumber, criteria.RegistrationNumber.Trim(), StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrWhiteSpace(criteria.Color) ||
                string.Equals(vehicle.Color, criteria.Color.Trim(), StringComparison.OrdinalIgnoreCase)) &&
            (!criteria.NumberOfWheels.HasValue || vehicle.NumberOfWheels == criteria.NumberOfWheels.Value) &&
            (criteria.VehicleType is null || criteria.VehicleType.IsInstanceOfType(vehicle)))
            .ToList();
    }
}



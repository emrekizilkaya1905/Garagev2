namespace Garagev2.Models;

public class Car : Vehicle
{
    public FuelType FuelType { get; }

    public Car(string registrationNumber, string color, int numberOfWheels, FuelType fuelType)
        : base(registrationNumber, color, numberOfWheels)
    {
        if (!Enum.IsDefined(fuelType))
        {
            throw new ArgumentOutOfRangeException(nameof(fuelType), "Fuel type must be a defined value.");
        }

        FuelType = fuelType;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Fuel type: {FuelType}";
    }
}


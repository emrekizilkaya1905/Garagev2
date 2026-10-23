using Garagev2.Models;
using static Garagev2.UI.ConsoleInput;

namespace Garagev2.UI;

public static class VehicleForm
{
    public static Vehicle ReadVehicle()
    {
        int type = ReadVehicleType(false);
        string registration = ReadRegistrationNumber();
        string color = ReadText("Color");
        int wheels = ReadInteger("Number of wheels", 0);
        return type switch
        {
            1 => new Motorcycle(registration, color, wheels, ReadInteger("Cylinder volume (cc)", 1)),
            2 => new Airplane(registration, color, wheels, ReadInteger("Number of engines", 1)),
            3 => new Car(registration, color, wheels, ReadFuelType()),
            4 => new Bus(registration, color, wheels, ReadInteger("Number of seats", 1)),
            5 => new Boat(registration, color, wheels, ReadLength()),
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Invalid vehicle type.")
        };
    }
}





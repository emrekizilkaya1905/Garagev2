namespace Garagev2.Models;

public class Boat : Vehicle
{
    public double Length { get; }

    public Boat(string registrationNumber, string color, int numberOfWheels, double length)
        : base(registrationNumber, color, numberOfWheels)
    {
        if (!double.IsFinite(length) || length <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Length must be a finite number greater than zero.");
        }

        Length = length;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Length: {Length} m";
    }
}


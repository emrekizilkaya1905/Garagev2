namespace Garagev2.Models;

public class Bus : Vehicle
{
    public int NumberOfSeats { get; }

    public Bus(string registrationNumber, string color, int numberOfWheels, int numberOfSeats)
        : base(registrationNumber, color, numberOfWheels)
    {
        if (numberOfSeats <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfSeats), "Number of seats must be greater than zero.");
        }

        NumberOfSeats = numberOfSeats;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Number of seats: {NumberOfSeats}";
    }
}


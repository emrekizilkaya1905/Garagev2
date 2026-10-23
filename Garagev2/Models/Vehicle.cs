using System.Text.RegularExpressions;
namespace Garagev2.Models;

public abstract class Vehicle
{
    public string RegistrationNumber { get; }
    public string Color { get; }
    public int NumberOfWheels { get; }

    protected Vehicle(string registrationNumber, string color, int numberOfWheels)
    {
        if (string.IsNullOrWhiteSpace(registrationNumber))
        {
            throw new ArgumentException("Registration number cannot be empty.", nameof(registrationNumber));
        }

        if (!IsValidRegistrationNumber(registrationNumber))
        {
            throw new ArgumentException("Registration number must match ABC123 or ABC12A.", nameof(registrationNumber));
        }

        if (string.IsNullOrWhiteSpace(color))
        {
            throw new ArgumentException("Color cannot be empty.", nameof(color));
        }

        if (numberOfWheels < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfWheels), "Number of wheels cannot be negative.");
        }

        RegistrationNumber = registrationNumber.Trim();
        Color = color.Trim();
        NumberOfWheels = numberOfWheels;
    }

    public static bool IsValidRegistrationNumber(string? registrationNumber)
    {
        return registrationNumber is not null &&
            Regex.IsMatch(registrationNumber.Trim(), @"\A[A-Za-z]{3}[0-9]{2}[A-Za-z0-9]\z");
    }

    public override string ToString()
    {
        return $"{GetType().Name} | Registration number: {RegistrationNumber} | Color: {Color} | Number of wheels: {NumberOfWheels}";
    }
}




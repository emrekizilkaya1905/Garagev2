# Garage 2.0

A C# console application for managing a garage with a fixed capacity. Built for the Lexicon Garage 2.0 exercise.

## Requirements

- .NET 10 SDK
- A terminal or an IDE that supports the project's .NET target

## Build and run

Open a terminal in the project directory containing `Garagev2.csproj`:

```powershell
dotnet build
dotnet run
```

Alternatively, from the solution directory containing `Garagev2.slnx`:

```powershell
dotnet run --project Garagev2/Garagev2.csproj
```

## Usage

At startup, enter a positive whole number to set the garage capacity. The garage is created once and remains active until you exit.

```text
1. List vehicles
2. List vehicle type counts
3. Park vehicle
4. Remove vehicle
5. Find by registration number
6. Search vehicles
7. Add sample vehicles
0. Exit
```

- **Park vehicle:** Select a vehicle type and enter its common and type-specific properties. Registration numbers must match `ABC123` or `ABC12A` (three letters, two digits, and a final letter or digit). Registration numbers must be unique within the garage, regardless of letter casing. Parking is rejected if the garage is full.
- **Remove vehicle:** Select a vehicle from the numbered list. Enter `0` to return without removing anything.
- **Find by registration number:** Enter a registration number. For example, `ABC123` and `Abc123` match the same vehicle.
- **Search vehicles:** Search by registration number, color, number of wheels, vehicle type, or multiple criteria. Single-criterion searches ask only for the selected property. For multiple criteria, leave a field blank and press Enter to skip it. All supplied filters must match. Leaving all filters blank returns every parked vehicle. Registration number and color comparisons ignore letter casing and use exact matches.
- **Add sample vehicles:** Attempt to park one vehicle of each supported type through the normal parking checks. Use a capacity of at least five in an empty garage to fit the complete sample set. Repeated attempts report duplicate registration numbers rather than adding duplicates.

Enter `/cancel` at a data-entry prompt to cancel the current operation and return to the main menu. At the startup capacity prompt, `/cancel` exits the application. Enter `0` at the main menu to exit.

Invalid numeric input is rejected with a message and another prompt. For boat length, use a decimal point, such as `8.5`.

## Vehicle types

All vehicles inherit the registration number, color, and number of wheels from the abstract `Vehicle` class.

| Type | Additional property |
| --- | --- |
| Motorcycle | Cylinder volume in cc |
| Airplane | Number of engines |
| Car | Fuel type: Gasoline or Diesel |
| Bus | Number of seats |
| Boat | Length in meters |

## Project structure

```text
Program.cs                     Application entry point
Models/                        Vehicle classes, FuelType enum, search criteria and operation result
Services/Garage.cs             Capacity, storage, parking, removal, lookup and filtering
UI/ConsoleUI.cs                Startup and main menu loop
UI/ConsoleInput.cs             Input reading and validation
UI/GarageMenuActions.cs        Menu operations and result presentation
UI/VehicleForm.cs              Vehicle creation from user input
UI/VehicleSearchForm.cs        Search menu and filter input
```

The garage stores vehicles in a private `List<Vehicle>`. Garage capacity is a separate, fixed limit checked before adding a vehicle; it is not the internal capacity of the list. Each vehicle occupies one space. Removing a vehicle removes its list entry and makes space available again. Listing and search methods return new lists so callers cannot modify the internal collection.

## Quick manual check

1. Start the application and enter a capacity of `5`.
2. Select `7` to add the sample vehicles.
3. Select `1` to list all five vehicles and their properties.
4. Select `5` and enter `abc123` to find the sample car.
5. Select `6`, then `2`, and enter `pink` to find the sample motorcycle.
6. Select `4` and choose a vehicle number to remove it.
7. Confirm that the number of used spaces decreases.
8. Select `0` to exit.

## Data lifetime

Garage data is stored in memory for the current session. Closing the application discards the data. File persistence and multiple garages are not implemented.

## Jag vill ha feedback på

Granska gärna branchen `review/garage-v2`. Jag önskar feedback på följande frågor:

- Är ansvarsfördelningen mellan UI-klasserna och `Garage` tydlig?
- Är `Vehicle` och dess subklasser utformade på ett bra sätt?
- Använder jag `List<Vehicle>` och hanterar garagets kapacitet korrekt?
- Fungerar sökning med flera valfria kriterier som förväntat?
- Är hanteringen av felaktig inmatning, ogiltiga registreringsnummer, dubbla registreringsnummer och fullt garage tillräckligt robust?

Ge gärna konkreta exempel med filnamn, metod och förbättringsförslag. Om du hittar en bugg, beskriv stegen för att återskapa den samt förväntat och faktiskt resultat.

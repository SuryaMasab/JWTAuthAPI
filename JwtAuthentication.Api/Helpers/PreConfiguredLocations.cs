using Authentication.Domain;

namespace JwtAuthentication.Api.Helpers;

public class PreConfiguredLocations
{ 
  public static List<Location> Locations { get; } =
    [
        new Location { Name = "LocationA", Latitude = 40.712776, Longitude = -74.005974 },
        new Location { Name = "LocationB", Latitude = 34.052235, Longitude = -118.243683 },
        // Add more locations as needed
    ];
}

namespace JwtAuthentication.Api.Helpers;
public class GeoUtils
{
    public static double AllowedDistanceInFeet { get; set; }
    private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double EarthRadius = 6371000; // Radius of the Earth in meters
        const double FeetToMeters = 0.3048; // Conversion factor from feet to meters

        // Convert degrees to radians
        double lat1Rad = DegreesToRadians(lat1);
        double lon1Rad = DegreesToRadians(lon1);
        double lat2Rad = DegreesToRadians(lat2);
        double lon2Rad = DegreesToRadians(lon2);

        // Approximate distance using Pythagorean theorem
        double dLat = lat2Rad - lat1Rad;
        double dLon = lon2Rad - lon1Rad;

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(lat1Rad) * Math.Cos(lat2Rad) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double distanceInMeters = EarthRadius * c;

        // Convert meters to feet and return
        return distanceInMeters / FeetToMeters;
    }

    private static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }

    public static bool IsWithinAllowedDistance(double userLat, double userLon)
    {
        foreach (var location in PreConfiguredLocations.Locations)
        {
            var distanceInFeet = CalculateDistance(userLat, userLon, location.Latitude, location.Longitude);
            if (distanceInFeet <= AllowedDistanceInFeet)
            {
                return true;
            }
        }

        return false;
    }
}

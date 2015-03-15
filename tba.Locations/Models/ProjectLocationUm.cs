using tba.Locations.Entities;

namespace tba.Locations.Models
{
    public class LocationUm 
    {
        public string Geohash { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string PostalCode { get; set; }

        public static LocationUm CreateLocationUm(Location location)
        {
            return (location != null)
             ? new LocationUm
             {
                 Geohash = location.Geohash,
                 AddressLine1 = location.AddressLine1,
                 AddressLine2 = location.AddressLine2,
                 City = location.City,
                 StateOrProvince = location.StateOrProvince,
                 PostalCode = location.PostalCode
             }
             : null;
        }

        public Location CreateUpdatedEntity(string locationId)
        {
            return (locationId != null)
            ? new Location
            {
                Id = locationId,
                Geohash = Geohash,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                City = City,
                StateOrProvince = StateOrProvince,
                PostalCode = PostalCode
            }
            : null;
        } 
    }
}
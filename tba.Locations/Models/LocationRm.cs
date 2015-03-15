using System.Collections.Generic;
using System.Linq;
using tba.Core.ViewModels;
using tba.Locations.Entities;

namespace tba.Locations.Models
{
    public class LocationRm : ReadOnlyViewModel<Location>
    {
        public string Geohash { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string PostalCode { get; set; }

        /// <summary>
        /// Map from a location entity to a location read-only view model
        /// </summary>
        /// <param name="location">a location entity</param>
        /// <returns></returns>
        public static LocationRm From(Location location)
        {
            return (location != null)
                ? new LocationRm
                {
                    Id = location.Id,
                    Geohash = location.Geohash,
                    AddressLine1 = location.AddressLine1,
                    AddressLine2 = location.AddressLine2,
                    City = location.City,
                    StateOrProvince = location.StateOrProvince,
                    PostalCode = location.PostalCode
                }
                : null;
        }

        /// <summary>
        /// Map a collection of location entities to an array of location
        /// read-only view models.
        /// </summary>
        /// <param name="entities">a collection of location entities</param>
        /// <returns> an array of location read-only view models</returns>
        public override ReadOnlyViewModel<Location>[] From(ICollection<Location> entities)
        {
            return entities.Select(From).ToArray();
        }
    }
}
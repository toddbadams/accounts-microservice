using System.ComponentModel.DataAnnotations;
using tba.Core.Entities;

namespace tba.Locations.Entities
{
    public class Location : Entity
    {
        /// <summary>
        /// Geohash is a latitude/longitude geocode system invented by Gustavo Niemeyer 
        /// when writing the web service at geohash.org, and put into the public domain. 
        /// It is a hierarchical spatial data structure which subdivides space into 
        /// buckets of grid shape.
        /// http://en.wikipedia.org/wiki/Geohash
        /// </summary>
        public string Geohash { get; set; }

        [MaxLength(100)]
        public string AddressLine1 { get; set; }

        [MaxLength(100)]
        public string AddressLine2 { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string StateOrProvince { get; set; }

        [MaxLength(20)]
        public string PostalCode { get; set; }

        public static Location Create()
        {
            return new Location
            {
                Id = NewBase64Id()
            };
        }

    }
}
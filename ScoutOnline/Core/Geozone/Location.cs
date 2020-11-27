using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutOnline.Core.Geozone
{
    public struct Location : IEquatable<Location>
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public Location(double latitude, double longitude) : this()
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public bool Equals(Location other)
        {
            return other.Latitude.Equals(Latitude) && other.Longitude.Equals(Longitude);
        }

        public override bool Equals(object obj)
        {
            return obj is Location && Equals((Location)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Latitude.GetHashCode() * 397) ^ Longitude.GetHashCode();
            }
        }

        public static bool operator ==(Location a, Location b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Location a, Location b)
        {
            return !a.Equals(b);
        }
    }
}

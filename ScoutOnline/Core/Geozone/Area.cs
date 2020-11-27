using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutOnline.Core.Geozone
{
    public struct Area : IEquatable<Area>
    {
        /// <summary>
        /// Копирует существующую область.
        /// </summary>
        /// <param name="area">Заданная область.</param>
        public Area(Area area)
            : this(area.NorthLatitude, area.WestLongitude, area.SouthLatitude, area.EastLongitude) { }

        /// <summary>
        /// Создает область по двум точкам на карте.
        /// </summary>
        /// <param name="leftTopPoint">Левая верхняя точка.</param>
        /// <param name="rightBottomPoint">Правая нижняя точка.</param>
        public Area(Location leftTopPoint, Location rightBottomPoint)
            : this(leftTopPoint.Latitude, leftTopPoint.Longitude, rightBottomPoint.Latitude, rightBottomPoint.Longitude) { }

        /// <summary>
        /// Создает область по граничным широтам/долготам.
        /// </summary>
        /// <param name="northLatitude">Широта северной границы области.</param>
        /// <param name="westLongitude">Долгота западной границы области.</param>
        /// <param name="southLatitude">Широта южной границы области.</param>
        /// <param name="eastLongitude">Долгота восточной границы области.</param>
        public Area(double northLatitude, double westLongitude, double southLatitude, double eastLongitude) : this()
        {
            NorthLatitude = northLatitude;
            WestLongitude = westLongitude;
            SouthLatitude = southLatitude;
            EastLongitude = eastLongitude;
        }

        /// <summary>
        /// Широта северной границы области.
        /// </summary>        
        public double NorthLatitude { get; private set; }

        /// <summary>
        /// Долгота западной границы области.
        /// </summary>        
        public double WestLongitude { get; private set; }

        /// <summary>
        /// Широта южной границы области.
        /// </summary>        
        public double SouthLatitude { get; private set; }

        /// <summary>
        /// Долгота восточной границы области.
        /// </summary>        
        public double EastLongitude { get; private set; }

        /// <summary>
        /// Создает минимальную прямоугольную область, содержащую все заданные точки.
        /// </summary>
        /// <param name="locations">Список точек на карте.</param>
        /// <returns>Прямоугольная область на карте.</returns>
        public static Area? GetOuterArea(IEnumerable<Location> locations)
        {
            if (locations == null) return null;
            double northLat = -90;
            double westLon = 180;
            double southLat = 90;
            double eastLon = -180;

            var hasLocations = false;
            foreach (var location in locations)
            {
                var lat = location.Latitude;
                var lon = location.Longitude;
                if (northLat < lat) northLat = lat;
                if (westLon > lon) westLon = lon;
                if (southLat > lat) southLat = lat;
                if (eastLon < lon) eastLon = lon;
                hasLocations = true;
            }

            return hasLocations ? new Area(northLat, westLon, southLat, eastLon) : (Area?)null;
        }

        public static Area? GetOuterArea(params Location[] locations)
        {
            var area = GetOuterArea((IEnumerable<Location>)locations);
            return area;
        }

        /// <summary>
        /// Создает минимальную прямоугольную область, полностью включающую в себя 2 заданные области.
        /// </summary>
        public static Area GetOuterArea(Area area1, Area area2)
        {
            return new Area(Math.Max(area1.NorthLatitude, area2.NorthLatitude),
                            Math.Min(area1.WestLongitude, area2.WestLongitude),
                            Math.Min(area1.SouthLatitude, area2.SouthLatitude),
                            Math.Max(area1.EastLongitude, area2.EastLongitude));
        }

        public Location GetCenter()
        {
            var centerLatitude = (SouthLatitude + NorthLatitude) / 2;
            var centerLongitude = (EastLongitude + WestLongitude) / 2;

            return new Location(centerLatitude, centerLongitude);
        }

        /// <summary>
        /// Проверяет, содержится ли в области заданная точка.
        /// </summary>
        /// <param name="location">Точка на карте.</param>
        /// <returns>True, если область содержит заданную точку и false в обратном случае.</returns>
        public bool Contains(Location location)
        {
            return (location.Latitude <= NorthLatitude &&
                    location.Latitude >= SouthLatitude &&
                    location.Longitude <= EastLongitude &&
                    location.Longitude >= WestLongitude);
        }

        public bool Contains(Area area)
        {
            return (area.NorthLatitude <= NorthLatitude &&
                    area.SouthLatitude >= SouthLatitude &&
                    area.EastLongitude <= EastLongitude &&
                    area.WestLongitude >= WestLongitude);
        }

        /// <summary>
        /// Показывает, пересекается ли область с другой областью.
        /// </summary>
        /// <param name="otherArea">Заданная область.</param>
        /// <returns>True, если области пересекаются, False - иначе.</returns>
        public bool IsIntersectsWith(Area otherArea)
        {
            return otherArea.EastLongitude >= WestLongitude &&
                   otherArea.WestLongitude <= EastLongitude &&
                   otherArea.SouthLatitude <= NorthLatitude &&
                   otherArea.NorthLatitude >= SouthLatitude;
        }

        public override bool Equals(object obj)
        {
            return obj is Area && Equals((Area)obj);
        }

        public bool Equals(Area other)
        {
            return other.NorthLatitude.Equals(NorthLatitude) && other.WestLongitude.Equals(WestLongitude) &&
                other.SouthLatitude.Equals(SouthLatitude) && other.EastLongitude.Equals(EastLongitude);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = NorthLatitude.GetHashCode();
                result = (result * 397) ^ WestLongitude.GetHashCode();
                result = (result * 397) ^ SouthLatitude.GetHashCode();
                result = (result * 397) ^ EastLongitude.GetHashCode();
                return result;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutOnline.Core.OnlineData
{
    public class OnlineDataInfo
    {
        public int UnitId { get; set; }
        public UnitConnectionStatus Status { get; set; }
        public UnitParkingStatus ParkingStatus { get; set; }
        public DateTime? Date { get; set; }
        public byte? SatellitesNumber { get; set; }
        public double? Speed { get; set; }
        public double? FuelLevel { get; set; }
        public bool? IsEngineWorking { get; set; }
        public string Address { get; set; }
        public string DriverName { get; set; }
        public int DriverId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Angle { get; set; }

        public bool CheckFilter(UnitsFilterType filter)
        {
            switch (filter)
            {
                case UnitsFilterType.All:
                    return true;
                case UnitsFilterType.Connected:
                    return Status == UnitConnectionStatus.Connected;
                case UnitsFilterType.Disconnected:
                    return Status == UnitConnectionStatus.Disconnected || Status == UnitConnectionStatus.Unknown;
                case UnitsFilterType.Lagging:
                    return Status == UnitConnectionStatus.Lagging;
                case UnitsFilterType.Moving:
                    return ParkingStatus == UnitParkingStatus.Moving;
                case UnitsFilterType.Parking:
                    return ParkingStatus == UnitParkingStatus.Stopped;

            }
            return false;
        }

        public override string ToString()
        {
            return $"{Date} {SatellitesNumber} {Speed} {FuelLevel} {Address} {DriverName}";
        }
    }

    public enum UnitConnectionStatus : byte
    {        
        Unknown = 0,     
        Connected = 1,        
        Lagging = 2,        
        Disconnected = 3,
    }

    public enum UnitParkingStatus : byte
    {
        Unknown = 0,
        Stopped = 1,
        Moving = 2
    }
}

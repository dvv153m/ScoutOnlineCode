using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutOnline.Core.OnlineData
{
    public class OnlineDataRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public UnitsFilterType? Filter { get; set; }
        public string Search { get; set; }
        public string OrderBy { get; set; }
        public bool? OrderDescending { get; set; }

        public int? Group { get; set; }
    }

    public enum UnitsFilterType
    {
        Unknown,
        All,
        Disconnected,
        Connected,
        Lagging,
        Moving,
        Parking
    }
}

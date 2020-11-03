using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutOnline.Core.Map
{
    public class MapInfo
    {
        public MapType Type { get; set; }

        public string Title { get; set; }

        public string BaseUrl { get; set; }

        public string[] Subdomains { get; set; }

        public int MaxZoom { get; set; }

        public string Attribution { get; set; }        
    }
}

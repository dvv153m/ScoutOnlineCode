using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutOnline.Core.Map
{
    public enum MapType : byte
    {
        Yandex = 0,
        YandexSatellite = 1,
        GoogleStreet = 2,
        GoogleHybrid = 3,
        GoogleSatellite = 4,
        Openstreet = 5,
        Wikimapia = 6
    }
}

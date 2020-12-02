using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutOnline.Core.Map
{
    public class MapsService
    {
        private Dictionary<MapType, MapInfo> _maps;
        public Dictionary<MapType, MapInfo> Maps 
        {
            get { return _maps; }
        }

        public MapsService()
        {
            _maps = new Dictionary<MapType, MapInfo>();

            _maps.Add(MapType.Openstreet, new MapInfo
            {
                Title = "OpenStreetMap",
                Type = MapType.Openstreet,
                BaseUrl = "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
                Subdomains = new string[] {"a", "b", "c"},
                MaxZoom = 19,
                Attribution = @"&copy; <a href=""https://www.openstreetmap.org/copyright"">OpenStreetMap</a>, Tiles courtesy of <a href=""https://hot.openstreetmap.org/"" target=""_blank"">Humanitarian OpenStreetMap Team</a>",
                Projection = BlazorLeaflet.Models.MapProjection.EPSG3857
            });

            _maps.Add(MapType.Yandex, new MapInfo
            {
                Title = "Yandex",
                Type = MapType.Yandex,
                BaseUrl = "https://vec{s}.maps.yandex.net/tiles?l=map&v=17.03.30-0&z={z}&x={x}&y={y}&scale=2",//lang=ru_RU
                Subdomains = new string[] { "01", "02", "03", "04" },
                MaxZoom = 19,
                Attribution = @"&copy; <a href=""http://yandex.ru"" target=""_blank"">Яндекс</a>",
                Projection = BlazorLeaflet.Models.MapProjection.EPSG3395
            });

            _maps.Add(MapType.YandexSatellite, new MapInfo
            {
                Title = "Yandex Спутник",
                Type = MapType.YandexSatellite,
                BaseUrl = "https://sat{s}.maps.yandex.net/tiles?l=sat&v=3.307.0&x={x}&y={y}&z={z}",
                Subdomains = new string[] { "01", "02", "03", "04" },
                MaxZoom = 17,
                Attribution = @"&copy; <a href=""http://yandex.ru"" target=""_blank"">Яндекс</a>",
                Projection = BlazorLeaflet.Models.MapProjection.EPSG3395
            });

            _maps.Add(MapType.GoogleStreet, new MapInfo
            {
                Title = "Google",
                Type = MapType.GoogleStreet,
                BaseUrl = "https://{s}.google.com/vt/lyrs=m&x={x}&y={y}&z={z}",
                Subdomains = new string[] { "mt0", "mt1", "mt2", "mt3" },
                MaxZoom = 20,                
                Attribution = @"&copy; <a href=""http://google.ru"" target=""_blank"">Google</a>",
                Projection = BlazorLeaflet.Models.MapProjection.EPSG3857
            });

            _maps.Add(MapType.GoogleHybrid, new MapInfo
            {
                Title = "Google Гибрид",
                Type = MapType.GoogleHybrid,
                BaseUrl = "https://{s}.google.com/vt/lyrs=s,h&x={x}&y={y}&z={z}",
                Subdomains = new string[] { "mt0", "mt1", "mt2", "mt3" },
                MaxZoom = 20,
                Attribution = @"&copy; <a href=""http://google.ru"" target=""_blank"">Google</a>",
                Projection = BlazorLeaflet.Models.MapProjection.EPSG3857
            });

            _maps.Add(MapType.GoogleSatellite, new MapInfo
            {
                Title = "Google Спутник",
                Type = MapType.GoogleSatellite,
                BaseUrl = "https://{s}.google.com/vt/lyrs=s&x={x}&y={y}&z={z}",
                Subdomains = new string[] { "mt0", "mt1", "mt2", "mt3" },
                MaxZoom = 20,
                Attribution = @"&copy; <a href=""http://google.ru"" target=""_blank"">Google</a>",
                Projection = BlazorLeaflet.Models.MapProjection.EPSG3857
            });

            _maps.Add(MapType.Wikimapia, new MapInfo
            {
                Title = "Wikimapia",
                Type = MapType.Wikimapia,
                BaseUrl = "http://{s}{s4}.wikimapia.org/?x={x}&y={y}&zoom={z}",
                Subdomains = new string[] { "i" },
                MaxZoom = 20,
                Attribution = @"&copy; <a href=""http://wikimapia.org"" target=""_blank"">Wikimapia.org</a>",
                Projection = BlazorLeaflet.Models.MapProjection.EPSG3857
            });
        }       
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorLeaflet.Models
{    
    /// <summary>
    /// Проекции карт https://leafletjs.com/reference-1.7.1.html#crs
    /// </summary>
    public enum MapProjection
    {
        EPSG3395 = 0,

        EPSG3857 = 1,

        EPSG4326 = 2,

        Earth = 3,

        Simple = 4,

        Base = 5
    }
}

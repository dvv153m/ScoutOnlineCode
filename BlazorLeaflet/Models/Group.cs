using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorLeaflet.Models
{
    public class Group : InteractiveLayer
    {
        public Polygon[] Objects { get; set; }
        
    }
}

namespace BlazorLeaflet.Models
{
    public class Polygon : Polyline
    {
        public override LayerType LayerType { get; set; } = LayerType.Polygon;
    }
}

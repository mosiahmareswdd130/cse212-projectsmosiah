/// <summary>
/// Maps to the top-level USGS GeoJSON response object.
/// The JSON has: { "type": "FeatureCollection", "features": [...] }
/// </summary>
public class FeatureCollection
{
    public List<Feature> Features { get; set; } = new();
}

/// <summary>
/// Maps to each item in the "features" array.
/// The JSON has: { "type": "Feature", "properties": { ... }, "geometry": { ... } }
/// </summary>
public class Feature
{
    public EarthquakeProperties Properties { get; set; } = new();
}

/// <summary>
/// Maps to the "properties" object inside each feature.
/// We only need "place" and "mag" for this assignment.
/// </summary>
public class EarthquakeProperties
{
    public string Place { get; set; } = "";
    public double Mag { get; set; }
}
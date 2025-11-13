// This class "FeatureCollection" is what helps create the JSON from the API used by grabbing all the other nested classes below
public class FeatureCollection
{
    public string Type { get; set; }
    public DataSet Metadata { get; set; }
    public List<EarthquakeEntry> Features { get; set; }

    // This class "Dataset" is what helps create the dataset of the earthquakes, like the URL, Title, Status, Api, time stamp of feed (generated), and Count, within the JSON
    public class DataSet
    {
        public long Generated { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string Api { get; set; }
        public int Count { get; set; }
    }

    // This class "EarthquakeEntry" is what helps actually show the data of each Earthquake entry, like the earthquake's magnitude place, time, update, etc
    public class EarthquakeEntry
    {
        public string Type { get; set; }
        public MagAndPlace Properties { get; set; }
    }

    // This class "MagAndPlace" is what helps grab the magnitude and place, because those are the only sets of data that the "EarthquakeDailySummary" class needs
    public class MagAndPlace
    {
        public double? Mag { get; set; }
        public string Place { get; set; }
    }
}
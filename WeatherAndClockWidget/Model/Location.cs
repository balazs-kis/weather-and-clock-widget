namespace WeatherAndClockWidget.Model
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Location(string latitude, string longitude)
        {
            try
            {
                Latitude = double.Parse(latitude);
                Longitude = double.Parse(longitude);
            }
            catch
            {
                Latitude = 47.4984;
                Longitude = 19.0406;
            }
        }

        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
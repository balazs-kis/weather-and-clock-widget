using System.Linq;

namespace WeatherAndClockWidget.Model
{
    public class WeatherData
    {
        public static WeatherData NoData => new WeatherData("", "0", "No data", "0", "0");

        public string LocationName { get; set; }
        public double Temperature { get; }
        public double Humidity { get; }
        public double Wind { get; }
        public string Conditions { get; }

        public WeatherData(string locationName, string temp, string condition, string hum, string wind)
        {
            try
            {
                LocationName = locationName;
                Temperature = double.Parse(temp) - 273.15;
                Humidity = double.Parse(hum);
                Wind = double.Parse(wind) * 3.6;
                Conditions = condition.First().ToString().ToUpper() + condition.Substring(1);
            }
            catch
            {
                Temperature = 0;
                Humidity = 0;
                Wind = 0;
                Conditions = "No data";
            }
        }
    }
}
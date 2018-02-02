using System.Linq;

namespace WeatherAndClockWidget.Model
{
    public class WeatherData
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Wind { get; set; }
        public string Conditions { get; set; }

        public WeatherData(string temp, string condition, string hum, string wind)
        {
            try
            {
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
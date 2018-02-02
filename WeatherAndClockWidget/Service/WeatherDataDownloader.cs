using Newtonsoft.Json.Linq;
using System.Net;
using WeatherAndClockWidget.Model;
using WeatherAndClockWidget.Service.Interface;

namespace WeatherAndClockWidget.Service
{
    public class WeatherDataDownloader : IWeatherDataDownloader
    {
        private const string LocationApiUrl = "https://freegeoip.net/json/";
        private const string CoordinatesPlaceholder = "[coordinates]";
        private readonly string _weatherApiUrl;

        public WeatherDataDownloader(IConfigReader configReader)
        {
            _weatherApiUrl = $"http://api.openweathermap.org/data/2.5/weather?{CoordinatesPlaceholder}&appid={configReader.WeatherApiKey}";
        }

        public WeatherData GetCurrentWeather()
        {
            try
            {
                WeatherData result;

                using (var wc = new WebClient())
                {
                    var location = GetLocation(wc);
                    result = GetWeatherData(wc, location);
                }

                return result;
            }
            catch
            {
                return null;
            }
        }

        private Location GetLocation(WebClient wc)
        {
            var response = JObject.Parse(wc.DownloadString(LocationApiUrl));

            var latitude = response["latitude"].ToString();
            var longitude = response["longitude"].ToString();

            return new Location(latitude, longitude);
        }

        private WeatherData GetWeatherData(WebClient wc, Location l)
        {
            var weatherApiUrl = _weatherApiUrl.Replace(CoordinatesPlaceholder, $"lat={l.Latitude:F4}&lon={l.Longitude:F4}");
            var resonse = JObject.Parse(wc.DownloadString(weatherApiUrl));

            var tempString = resonse["main"]["temp"].ToString();
            var conditionString = resonse["weather"][0]["main"].ToString();
            var humString = resonse["main"]["humidity"].ToString();
            var windString = resonse["wind"]["speed"].ToString();

            return new WeatherData(tempString, conditionString, humString, windString);
        }
    }
}
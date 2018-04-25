using Newtonsoft.Json.Linq;
using System;
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

        public WeatherData GetCurrentWeather() =>
            Disposable
                .Using(CreateWebClient, client => client.DownloadString(LocationApiUrl))
                .Map(JObject.Parse)
                .Map(GetLocationFromJson)
                .Map(location => _weatherApiUrl.Replace(CoordinatesPlaceholder, $"lat={location.Latitude:F4}&lon={location.Longitude:F4}"))
                .Using(CreateWebClient, (client, uri) => client.DownloadString(uri))
                .Map(JObject.Parse)
                .Map(GetWeatherDataFromJson);

        private static WebClient CreateWebClient() =>
            new WebClient();

        private static Location GetLocationFromJson(JObject o) =>
            new Location(o["latitude"].ToString(), o["longitude"].ToString());

        private static WeatherData GetWeatherDataFromJson(JObject o) =>
            new WeatherData(
                o["main"]["temp"].ToString(),
                o["weather"][0]["main"].ToString(),
                o["main"]["humidity"].ToString(),
                o["wind"]["speed"].ToString());
    }
}
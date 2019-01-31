using Newtonsoft.Json.Linq;
using System;
using System.Net;
using WeatherAndClockWidget.Model;
using WeatherAndClockWidget.Service.Interface;

namespace WeatherAndClockWidget.Service
{
    public sealed class GetWeatherFunction : IGetWeatherFunction
    {
        public WeatherData GetCurrentWeather(string locationApiUrl, string weatherApiUrl, string latitudePlaceholder, string longitudePlaceholder) =>
            Disposable
                .Using(CreateWebClient, client => client.DownloadString(locationApiUrl))
                .Map(JObject.Parse)
                .Map(GetLocationFromJson)
                .Map(location => weatherApiUrl.Replace(latitudePlaceholder, location.Latitude.ToString("F4")).Replace(longitudePlaceholder, location.Longitude.ToString("F4")))
                .Using(CreateWebClient, (client, uri) => client.DownloadString(uri))
                .Map(JObject.Parse)
                .Map(GetWeatherDataFromJson);

        private static WebClient CreateWebClient() =>
            new WebClient();

        private static Location GetLocationFromJson(JObject o) =>
            new Location(o["latitude"].ToString(), o["longitude"].ToString());

        private static WeatherData GetWeatherDataFromJson(JObject o) =>
            new WeatherData(
                o["name"].ToString(),
                o["main"]["temp"].ToString(),
                o["weather"][0]["main"].ToString(),
                o["main"]["humidity"].ToString(),
                o["wind"]["speed"].ToString());
    }
}
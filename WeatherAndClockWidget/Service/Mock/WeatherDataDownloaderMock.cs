using System;
using WeatherAndClockWidget.Model;
using WeatherAndClockWidget.Service.Interface;

namespace WeatherAndClockWidget.Service.Mock
{
    public class WeatherDataDownloaderMock : IWeatherDataDownloader
    {
        private readonly Random _random = new Random();

        public WeatherData GetCurrentWeather()
        {
            var temp = (double) _random.Next(2800, 3000) / 10;
            var hum = (double)_random.Next(0, 1000) / 10;
            var wind = (double) _random.Next(0, 200) / 10;
            return new WeatherData(temp.ToString("F1"), "Overcast", hum.ToString("F1"), wind.ToString("F1"));
        }
    }
}
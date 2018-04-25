using System;
using System.Configuration;
using WeatherAndClockWidget.Service.Interface;

namespace WeatherAndClockWidget.Service
{
    public class ConfigReader : IConfigReader
    {
        public string WeatherApiKey =>
            ConfigurationManager.AppSettings["WeatherApiKey"];

        public TimeSpan WeatherUpdatePeriod =>
            ConfigurationManager
                .AppSettings["WeatherUpdatePeriod"]
                .Map(s => ParseWithDefault(s, 15))
                .When(periodInMinutes => periodInMinutes > 120, periodInMinutes => 120)
                .When(periodInMinutes => periodInMinutes < 5, periodInMinutes => 5)
                .Map(periodInMinutes => TimeSpan.FromMinutes(periodInMinutes));


        private static int ParseWithDefault(string toParse, int defaultValue = 0) =>
            int.TryParse(toParse, out var periodInMinutes)
                ? periodInMinutes
                : defaultValue;
    }
}
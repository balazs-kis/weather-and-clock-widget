using System;

namespace WeatherAndClockWidget.Service.Interface
{
    public interface IConfigReader
    {
        TimeSpan WeatherUpdatePeriod { get; }
        string WeatherApiKey { get; }
    }
}
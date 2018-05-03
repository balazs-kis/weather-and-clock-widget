using System;
using WeatherAndClockWidget.Service.Interface;

namespace WeatherAndClockWidget.Service.Mock
{
    public class ConfigReaderMock : IConfigReader
    {
        public TimeSpan WeatherUpdatePeriod  => TimeSpan.FromMinutes(1);
        public string WeatherApiKey => "MockApiKey";
        public string LocationApiKey => "MockApiKey";
    }
}
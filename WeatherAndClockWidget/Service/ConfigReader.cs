using System;
using System.Configuration;
using WeatherAndClockWidget.Service.Interface;

namespace WeatherAndClockWidget.Service
{
    public class ConfigReader : IConfigReader
    {
        public TimeSpan WeatherUpdatePeriod
        {
            get
            {
                var conf = ConfigurationManager.AppSettings["WeatherUpdatePeriod"];
                var isParsed = int.TryParse(conf, out var periodInMinutes);

                // Cannot parse value: use the default 15 minutes.
                if (!isParsed)
                {
                    return TimeSpan.FromMinutes(15);
                }

                // The maximum of the update period: 2 hours.
                if (periodInMinutes > 120)
                {
                    periodInMinutes = 120;
                }

                // The minimum of the update period: 5 minutes.
                if (periodInMinutes < 5)
                {
                    periodInMinutes = 5;
                }

                return TimeSpan.FromMinutes(periodInMinutes);
            }
        }

        public string WeatherApiKey => ConfigurationManager.AppSettings["WeatherApiKey"];
    }
}
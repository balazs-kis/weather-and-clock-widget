using WeatherAndClockWidget.Model;
using WeatherAndClockWidget.Service.Interface;

namespace WeatherAndClockWidget.Service
{
    public class WeatherDataDownloader : IWeatherDataDownloader
    {
        private const string LatitudePlaceholder = "[lat]";
        private const string LongitudePlaceholder = "[lon]";

        private readonly string _locationApiUrl;
        private readonly string _weatherApiUrl;
        private readonly IGetWeatherFunction _getWeatherFunction;

        public WeatherDataDownloader(IConfigReader configReader, IGetWeatherFunction getWeatherFunction)
        {
            _locationApiUrl = $"http://api.ipstack.com/check?access_key={configReader.LocationApiKey}";
            _weatherApiUrl = $"http://api.openweathermap.org/data/2.5/weather?lat=[lat]&lon=[lon]&appid={configReader.WeatherApiKey}";

            _getWeatherFunction = getWeatherFunction;
        }

        public WeatherData GetCurrentWeather() =>
            _getWeatherFunction.GetCurrentWeather(_locationApiUrl, _weatherApiUrl, LatitudePlaceholder, LongitudePlaceholder);
    }
}
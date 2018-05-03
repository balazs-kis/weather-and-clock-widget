using WeatherAndClockWidget.Model;
using WeatherAndClockWidget.Service.Interface;

namespace WeatherAndClockWidget.Service.Mock
{
    public class GetWeatherFunctionMock : IGetWeatherFunction
    {
        public WeatherData GetCurrentWeather(string locationApiUrl, string weatherApiUrl, string latitudePlaceholder, string longitudePlaceholder)
        {
            return WeatherData.NoData;
        }
    }
}
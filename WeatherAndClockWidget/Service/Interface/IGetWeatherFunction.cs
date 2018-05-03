using WeatherAndClockWidget.Model;

namespace WeatherAndClockWidget.Service.Interface
{
    public interface IGetWeatherFunction
    {
        WeatherData GetCurrentWeather(string locationApiUrl, string weatherApiUrl, string latitudePlaceholder, string longitudePlaceholder);
    }
}
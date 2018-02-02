using WeatherAndClockWidget.Model;

namespace WeatherAndClockWidget.Service.Interface
{
    public interface IWeatherDataDownloader
    {
        WeatherData GetCurrentWeather();
    }
}
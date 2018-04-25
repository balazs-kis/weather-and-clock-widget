using WeatherAndClockWidget.Model;

namespace WeatherAndClockWidget.Service.Interface
{
    public interface IStatePersister
    {
        void SaveState(State state);
        State GetSavedState();
    }
}
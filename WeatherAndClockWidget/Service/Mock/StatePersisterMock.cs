using WeatherAndClockWidget.Model;
using WeatherAndClockWidget.Service.Interface;

namespace WeatherAndClockWidget.Service.Mock
{
    public class StatePersisterMock : IStatePersister
    {
        public void SaveState(State s)
        {
        }

        public State GetSavedState()
        {
            return State.Default;
        }
    }
}
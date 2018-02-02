using System.Threading.Tasks;
using WeatherAndClockWidget.Model;
using WeatherAndClockWidget.Service.Interface;

namespace WeatherAndClockWidget.Service.Mock
{
    public class StatePersisterMock : IStatePersister
    {
        public Task SaveState(State s)
        {
            return Task.FromResult(true);
        }

        public Task<State> GetSavedState()
        {
            return Task.FromResult(new State
            {
                IsLocked = false,
                IsVisible = true,
                Left = 10,
                Top = 10
            });
        }
    }
}
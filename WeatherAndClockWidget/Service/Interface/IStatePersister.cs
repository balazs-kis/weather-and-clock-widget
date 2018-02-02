using System.Threading.Tasks;
using WeatherAndClockWidget.Model;

namespace WeatherAndClockWidget.Service.Interface
{
    public interface IStatePersister
    {
        Task SaveState(State s);
        Task<State> GetSavedState();
    }
}
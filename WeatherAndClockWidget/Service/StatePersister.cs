using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using WeatherAndClockWidget.Model;
using WeatherAndClockWidget.Service.Interface;

namespace WeatherAndClockWidget.Service
{
    public class StatePersister : IStatePersister
    {
        private const string SavedSettingsPath = "saved-state.json";
        
        public Task SaveState(State s)
        {
            return Task.Run(() =>
            {
                var savedStateJson = JsonConvert.SerializeObject(s);
                File.WriteAllText(SavedSettingsPath, savedStateJson);
            });
        }

        public Task<State> GetSavedState()
        {
            return Task.Run(() =>
            {
                if (!File.Exists(SavedSettingsPath))
                {
                    return null;
                }

                try
                {
                    var savedStateJson = File.ReadAllText(SavedSettingsPath);
                    var savedState = JsonConvert.DeserializeObject<State>(savedStateJson);

                    return savedState;
                }
                catch
                {
                    return null;
                }
            });
        }
    }
}
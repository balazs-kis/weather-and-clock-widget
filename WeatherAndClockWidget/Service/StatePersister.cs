using Newtonsoft.Json;
using System;
using System.IO;
using WeatherAndClockWidget.Model;
using WeatherAndClockWidget.Service.Interface;

namespace WeatherAndClockWidget.Service
{
    public class StatePersister : IStatePersister
    {
        private const string SavedSettingsPath = "saved-state.json";

        public void SaveState(State state) =>
            state
                .Map(JsonConvert.SerializeObject)
                .Tee(json => File.WriteAllText(SavedSettingsPath, json));


        public State GetSavedState() =>
            File.Exists(SavedSettingsPath)
                ? File
                    .ReadAllText(SavedSettingsPath)
                    .Map(JsonConvert.DeserializeObject<State>)
                : State.Default;
    }
}
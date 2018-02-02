using GalaSoft.MvvmLight.Messaging;

namespace WeatherAndClockWidget.Message
{
    public class StateMessage : MessageBase
    {
        public bool IsUnlocked { get; set; }
    }
}
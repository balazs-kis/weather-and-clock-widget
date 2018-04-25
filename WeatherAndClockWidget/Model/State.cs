using Newtonsoft.Json;

namespace WeatherAndClockWidget.Model
{
    public class State
    {
        public static State Default = new State(false, true, 10, 10);

        public bool IsLocked { get; }
        public bool IsVisible { get; }
        public double Left { get; }
        public double Top { get; }

        public State(State from) :
            this(from.IsLocked, from.IsVisible, from.Left, from.Top)
        {
        }

        [JsonConstructor]
        public State(bool isLocked, bool isVisible, double left, double top)
        {
            IsLocked = isLocked;
            IsVisible = isVisible;
            Left = left;
            Top = top;
        }
    }
}
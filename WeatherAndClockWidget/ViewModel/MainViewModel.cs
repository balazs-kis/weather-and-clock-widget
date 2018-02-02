using GalaSoft.MvvmLight;
using System;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using WeatherAndClockWidget.Message;
using WeatherAndClockWidget.Service.Interface;

namespace WeatherAndClockWidget.ViewModel
{
    /// <summary>
    /// Main ViewModel of the application.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IWeatherDataDownloader _downloader;

        private bool _isUnlocked;
        private DateTime _time;
        private double _temperature;
        private string _conditions;
        private double _windSpeed;
        private double _humidity;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(
            IWeatherDataDownloader weatherDataDownloader,
            IStatePersister statePersister,
            IConfigReader configReader)
        {
            _downloader = weatherDataDownloader;
            var savedState = statePersister.GetSavedState().GetAwaiter().GetResult();

            IsUnlocked = savedState == null ? true : !savedState.IsLocked;
            Messenger.Default.Register<StateMessage>(this, m => IsUnlocked = m.IsUnlocked);

            UpdateTimeAndDate();
            UpdateWeather(configReader.WeatherUpdatePeriod);
        }

        public bool IsUnlocked
        {
            get { return _isUnlocked; }
            set
            {
                _isUnlocked = value;
                RaisePropertyChanged();
            }
        }

        public DateTime Time
        {
            get => _time;
            set
            {
                _time = value;
                RaisePropertyChanged();
            }
        }

        public double Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                RaisePropertyChanged();
            }
        }

        public string Conditions
        {
            get => _conditions;
            set
            {
                _conditions = value;
                RaisePropertyChanged();
            }
        }

        public double WindSpeed
        {
            get => _windSpeed;
            set
            {
                _windSpeed = value;
                RaisePropertyChanged();
            }
        }

        public double Humidity
        {
            get => _humidity;
            set
            {
                _humidity = value;
                RaisePropertyChanged();
            }
        }


        private void UpdateTimeAndDate()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Time = DateTime.Now;
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                }
                // ReSharper disable once FunctionNeverReturns
            }, TaskCreationOptions.LongRunning).ConfigureAwait(false);
        }

        private void UpdateWeather(TimeSpan updatePeriod)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    var wd = _downloader.GetCurrentWeather();
                    if (wd != null)
                    {
                        Temperature = wd.Temperature;
                        Conditions = wd.Conditions;
                        WindSpeed = wd.Wind;
                        Humidity = wd.Humidity;
                    }

                    Thread.Sleep(updatePeriod);
                }
                // ReSharper disable once FunctionNeverReturns
            }, TaskCreationOptions.LongRunning).ConfigureAwait(false);
        }
    }
}
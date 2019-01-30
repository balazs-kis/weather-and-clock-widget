using GalaSoft.MvvmLight.Messaging;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WeatherAndClockWidget.Message;
using WeatherAndClockWidget.Model;
using WeatherAndClockWidget.Service;
using WeatherAndClockWidget.Service.Interface;
using Timer = System.Timers.Timer;

namespace WeatherAndClockWidget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly TimeSpan _transitionTime;
        private readonly IStatePersister _statePersister;
        private readonly Timer _timer;

        private MenuItem _lockMenuItem, _showMenuItem;
        private State _savedState;


        public MainWindow()
        {
            _transitionTime = TimeSpan.FromMilliseconds(700);
            _statePersister = new StatePersister();
            _timer = new Timer(5000) { AutoReset = true, Enabled = true };

            _savedState = _statePersister.GetSavedState();

            SetInitialState(_savedState);
            CreateTrayIcon(_savedState);

            InitializeComponent();

            _timer.Elapsed += MainWindow_RepositionIfNeeded;
            _timer.Start();
        }


        private void TrayIcon_OnCloseClicked(object sender, EventArgs e)
        {
            SaveState();
            System.Windows.Application.Current.Shutdown();
        }

        private void TrayIcon_OnLockSwitched(object sender, EventArgs e)
        {
            var mi = (MenuItem)sender;

            mi.Checked = !mi.Checked;
            Messenger.Default.Send(new StateMessage(!mi.Checked));

            SaveState();
        }

        private void TrayIcon_OnHideSwitched(object sender, EventArgs e)
        {
            var mi = (MenuItem)sender;
            mi.Checked = !mi.Checked;

            if (mi.Checked)
            {
                ShowWindowWithTransition();
            }
            else
            {
                HideWindowWithTransition();
            }

            SaveState();
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && !_lockMenuItem.Checked)
            {
                DragMove();
            }
        }

        private void MainWindow_RepositionIfNeeded(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    if (_savedState.Left == Left && _savedState.Top == Top)
                    {
                        return;
                    }

                    var currentWidth = SystemParameters.VirtualScreenWidth;
                    var currentHeight = SystemParameters.VirtualScreenHeight;

                    if (_savedState.Left > currentWidth || _savedState.Top > currentHeight)
                    {
                        return;
                    }

                    Left = _savedState.Left;
                    Top = _savedState.Top;
                }
                catch
                {
                    // Cannot read screen parameters; the next iteration will retry.
                }
            });
        }


        private void SaveState()
        {
            _savedState = new State(_lockMenuItem.Checked, _showMenuItem.Checked, Left, Top);
            _statePersister.SaveState(_savedState);
        }


        private void SetInitialState(State s)
        {
            Visibility = s.IsVisible ? Visibility.Visible : Visibility.Hidden;

            WindowStartupLocation = WindowStartupLocation.Manual;
            Left = s.Left;
            Top = s.Top;
        }

        private void CreateTrayIcon(State s)
        {
            _lockMenuItem =
                new MenuItem(Properties.Resources.LockText, TrayIcon_OnLockSwitched)
                {
                    Checked = s.IsLocked
                };

            _showMenuItem =
                new MenuItem(Properties.Resources.ShowText, TrayIcon_OnHideSwitched)
                {
                    Checked = s.IsVisible
                };

            var notifyIcon = new NotifyIcon
            {
                Visible = true,
                Text = Properties.Resources.AppName,
                Icon = new Icon("icon.ico"),
                ContextMenu = new ContextMenu(new[]
                {
                    _lockMenuItem,
                    _showMenuItem,
                    new MenuItem("-"),
                    new MenuItem(Properties.Resources.CloseText, TrayIcon_OnCloseClicked)
                })
            };
        }


        private void ShowWindowWithTransition()
        {
            Visibility = Visibility.Visible;

            BeginAnimation(OpacityProperty, new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(_transitionTime),
                AutoReverse = false
            });
        }

        private void HideWindowWithTransition()
        {
            BeginAnimation(OpacityProperty, new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = new Duration(_transitionTime),
                AutoReverse = false
            });

            Task.Run(() => { Thread.Sleep(_transitionTime); })
                .ContinueWith(r => Visibility = Visibility.Hidden, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
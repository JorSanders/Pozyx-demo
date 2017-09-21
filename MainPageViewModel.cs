using System;

using FunkyClient.Pozyx;

using Windows.UI.Core;
using Windows.UI.Xaml;

using Platform.ComponentModel;

namespace FunkyClient
{
    /// <summary>
    /// Main page view model
    /// </summary>
    public class MainPageViewModel : ObservableBase
    {
        private PozyxDevice _Device;
        private CoreDispatcher _Dispatcher;
        private DispatcherTimer _Timer;


        /// <summary>
        /// Construction
        /// </summary>
        public MainPageViewModel()
        {
            _Device = new PozyxDevice();
            _Timer = new DispatcherTimer();
            _Timer.Interval = TimeSpan.FromMilliseconds(100);
            _Timer.Tick += OnTimerTick;
            _Timer.Start();
        }

        private void OnTimerTick(object sender, object e)
        {
            Date = _Device?.Position?.Date.ToString("HH:mm:ss.fffff");
            X = _Device?.Position?.X;
            Y = _Device?.Position?.Y;
        }

        /// <summary>
        /// Gets or sets the X coordinate
        /// </summary>
        public string Date
        {
            get => _Date;
            set => SetPropertyValue(ref _Date, value, nameof(Date));
        }
        private string _Date;

        /// <summary>
        /// Gets or sets the X coordinate
        /// </summary>
        public int? X
        {
            get => _X;
            set => SetPropertyValue(ref _X, value, nameof(X));
        }
        private int? _X;

        /// <summary>
        /// Gets or sets the Y coordinate
        /// </summary>
        public int? Y
        {
            get => _Y;
            set => SetPropertyValue(ref _Y, value, nameof(Y));
        }
        private int? _Y;
    }
}

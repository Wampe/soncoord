using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Soncoord.Client.WPF
{
    public class ShellViewModel : BindableBase
    {
        private readonly DispatcherTimer _positionTimer;

        public ShellViewModel()
        {
            _positionTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle)
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };
            _positionTimer.Tick += PositionTimerTick;

            TimeCodes = new ObservableCollection<KeyValuePair<TimeSpan, IList<string>>>
            {
                new KeyValuePair<TimeSpan, IList<string>>(
                    TimeSpan.FromSeconds(3),
                    new List<string>
                    {
                        "Command 1",
                        "Command 2"
                    }
                ),
                new KeyValuePair<TimeSpan, IList<string>>(
                    TimeSpan.FromSeconds(8),
                    new List<string>
                    {
                        "Command 3",
                        "Command 4",
                        "Command 5"
                    }
                ),
                new KeyValuePair<TimeSpan, IList<string>>(
                    TimeSpan.FromSeconds(12),
                    new List<string>
                    {
                        "Command 6"
                    }
                ),
                new KeyValuePair<TimeSpan, IList<string>>(
                    TimeSpan.FromSeconds(15),
                    new List<string>
                    {
                        "Command 7",
                        "Command 8"
                    }
                ),
                new KeyValuePair<TimeSpan, IList<string>>(
                    TimeSpan.FromSeconds(18),
                    new List<string>
                    {
                        "Command 9",
                        "Command 10",
                        "Command 11",
                        "Command 12"
                    }
                ),
            };

            _positionTimer.Start();
        }

        public ObservableCollection<KeyValuePair<TimeSpan, IList<string>>> TimeCodes { get; set; }

        private TimeSpan _playerTime;
        public TimeSpan PlayerTime
        {
            get => _playerTime;
            set => SetProperty(ref _playerTime, value);
        }

        private int _timeCodeIndex;
        public int TimeCodeIndex
        {
            get => _timeCodeIndex;
            set => SetProperty(ref _timeCodeIndex, value);
        }

        private void PositionTimerTick(object sender, EventArgs e)
        {
            PlayerTime = TimeSpan.FromSeconds(PlayerTime.Seconds+1);

            if (TimeSpan.Compare(PlayerTime, TimeCodes[TimeCodeIndex].Key) >= 0)
            {
                foreach (var item in TimeCodes[TimeCodeIndex].Value)
                {
                    Console.WriteLine($"Fire specific event on {TimeCodes[TimeCodeIndex].Key}: {item}");
                }
                
                TimeCodeIndex++;
            }

            if (TimeSpan.Compare(PlayerTime, TimeCodes[TimeCodes.Count - 1].Key) >= 0)
            {
                _positionTimer.Stop();
                PlayerTime = new TimeSpan(0);
                TimeCodeIndex = 0;
                Console.WriteLine($"Timer stopped at {TimeCodes[TimeCodes.Count - 1].Key}");
            }
        }
    }
}

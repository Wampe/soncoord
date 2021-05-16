using NAudio.Extras;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Soncoord.Business.Player
{
    public class PlayerExecuter : IPlayerExecuter
    {
        private readonly DispatcherTimer _positionTimer;
        private readonly IOutputsService _outputsService;
        private int _timeCodeCommandIndex { get; set; }
        private ObservableCollection<KeyValuePair<TimeSpan, IList<string>>> _timeCodeCommands { get; set; }

        private DirectSoundOut _clickTrackOutput;
        private DirectSoundOut _songTrackOutput;
        private AudioFileReader _clickTrackReader;
        private AudioFileReader _songTrackReader;

        public PlayerExecuter(IOutputsService outputsService)
        {
            _outputsService = outputsService;
            _positionTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle)
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };
            _positionTimer.Tick += PositionTimerTick;

            UpdateAudioPosition(new TimeSpan(0));

            _timeCodeCommands = new ObservableCollection<KeyValuePair<TimeSpan, IList<string>>>
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
        }

        public event EventHandler<TimeSpan> PositionChanged;
        public event EventHandler<TimeSpan> Started;
        public event EventHandler Stopped;
        public event EventHandler Ended;

        public bool IsTrackRunReverted { get; set; }

        public void Play(ISongSetting settings)
        {
            Play(settings.ClickTrackPath, settings.MusicTrackPath);
        }

        public void Play(string clickTrack, string songTrack)
        {
            var selectedOutputSettings = _outputsService.GetSettings();

            _clickTrackReader = new AudioFileReader(clickTrack);
            _clickTrackOutput = new DirectSoundOut(selectedOutputSettings.ClickTrackOutputDevice.Guid, 300);
            _clickTrackOutput.Init(_clickTrackReader.ToSampleProvider());

            _songTrackReader = new AudioFileReader(songTrack);
            _songTrackOutput = new DirectSoundOut(selectedOutputSettings.SongTrackOutputDevice.Guid, 300);
            _songTrackOutput.PlaybackStopped += OnPlaybackStopped;

            var songTrackProvider = new OffsetSampleProvider(_songTrackReader.ToSampleProvider());
            int sampleRate = songTrackProvider.WaveFormat.SampleRate;
            int channels = songTrackProvider.WaveFormat.Channels;
            var delay = _clickTrackReader.TotalTime - _songTrackReader.TotalTime;
            var samplesToDelay = Convert.ToInt32(sampleRate * delay.TotalSeconds) * channels;
            songTrackProvider.DelayBySamples = samplesToDelay;

            _songTrackOutput.Init(
                new Equalizer(
                    songTrackProvider,
                    selectedOutputSettings.EqualizerBands));
            
            _timeCodeCommandIndex = 0;
            _clickTrackOutput.Play();
            _songTrackOutput.Play();
            _positionTimer.Start();

            Started?.Invoke(this, _songTrackReader.TotalTime);
        }

        public void Stop()
        {
            _songTrackOutput?.Stop();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            _songTrackOutput.PlaybackStopped -= OnPlaybackStopped;
            _songTrackOutput.Dispose();
            _songTrackOutput = null;

            _clickTrackOutput.Stop();
            _clickTrackOutput.Dispose();
            _clickTrackOutput = null;

            _positionTimer.Stop();
            UpdateAudioPosition(new TimeSpan(0));
            if (_songTrackReader.CurrentTime < _songTrackReader.TotalTime)
            {
                Stopped?.Invoke(this, null);
            }
            else
            {
                Ended?.Invoke(this, null);
            }
        }

        private void PositionTimerTick(object sender, EventArgs e)
        {
            var position = IsTrackRunReverted
                    ? _songTrackReader.TotalTime - _songTrackReader.CurrentTime
                    : _songTrackReader.CurrentTime;

            UpdateAudioPosition(position);

            // Problem: 
            // This current (dummy) implementation already hits the timer intervall for updarting the time display in the controller
            // and causes lags in updating the UI. 
            //
            // ToDo: CHECK FOR Background Thread based handling
            // Commands will just trigger specific non-UI things so it should be possible (and enough) to to this in background!
            //if (_timeCodeCommandIndex != -1)
            //{
            //    TimeCodeCommands(position);
            //}
        }

        private void UpdateAudioPosition(TimeSpan value)
        {
            PositionChanged?.Invoke(this, value);
        }

        private void TimeCodeCommands(TimeSpan position)
        {
            if (TimeSpan.Compare(position, _timeCodeCommands[_timeCodeCommandIndex].Key) >= 0)
            {
                foreach (var item in _timeCodeCommands[_timeCodeCommandIndex].Value)
                {
                    Console.WriteLine($"Fire specific event on {_timeCodeCommands[_timeCodeCommandIndex].Key}: {item}");
                }

                _timeCodeCommandIndex++;
            }

            if (TimeSpan.Compare(position, _timeCodeCommands[_timeCodeCommands.Count - 1].Key) >= 0)
            {
                _timeCodeCommandIndex = -1;
                Console.WriteLine($"Timer stopped at {_timeCodeCommands[_timeCodeCommands.Count - 1].Key}");
            }
        }
    }
}

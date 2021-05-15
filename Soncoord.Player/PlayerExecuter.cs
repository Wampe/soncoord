using NAudio.Wave;
using NAudio.Extras;
using System;
using System.Windows.Threading;
using NAudio.Wave.SampleProviders;
using Soncoord.Infrastructure.Interfaces;
using System.IO;
using Newtonsoft.Json;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Models;

namespace Soncoord.Player
{
    public class PlayerExecuter
    {
        private DirectSoundOut _outputClick;
        private DirectSoundOut _outputSong;
        private AudioFileReader _clickReader;
        private AudioFileReader _songReader;

        private readonly DispatcherTimer _positionTimer;

        public PlayerExecuter()
        {
            _positionTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle)
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };
            _positionTimer.Tick += PositionTimerTick;

            UpdateAudioPosition(new TimeSpan(0));
        }
        
        public event EventHandler<TimeSpan> PositionUpdated;
        public event EventHandler<TimeSpan> Started;
        public bool IsRevertedTrackRun { get; set; }
        internal TimeSpan AudioPosition { get; private set; }

        public void Play(string clickTrackPath, string songTrackPath)
        {
            var selectedOutputSettings = LoadPlayerOutputSettings();

            _clickReader = new AudioFileReader(clickTrackPath);
            _outputClick = new DirectSoundOut(selectedOutputSettings.ClickTrackOutputDevice.Guid);
            _outputClick.PlaybackStopped += OnPlaybackStopped;
            _outputClick.Init(
                new OffsetSampleProvider(_clickReader.ToSampleProvider())
                {
                    DelayBy = TimeSpan.FromSeconds(1)
                });

            _songReader = new AudioFileReader(songTrackPath);
            _outputSong = new DirectSoundOut(selectedOutputSettings.SongTrackOutputDevice.Guid);
            _outputSong.PlaybackStopped += OnPlaybackStopped;
            _outputSong.Init(
                new Equalizer(
                    new OffsetSampleProvider(_songReader.ToSampleProvider())
                    {
                        DelayBy = _clickReader.TotalTime - _songReader.TotalTime + TimeSpan.FromSeconds(1)
                    },
                    selectedOutputSettings.EqualizerBands));

            _outputClick.Play();
            _outputSong.Play();
            _positionTimer.Start();

            Started?.Invoke(this, _songReader.TotalTime);
        }

        public void Stop()
        {
            _outputClick?.Stop();
            _outputSong?.Stop();
            
            _clickReader.Dispose();
            _clickReader = null;

            _songReader.Dispose();
            _songReader = null;

            _positionTimer.Stop();
            UpdateAudioPosition(new TimeSpan(0));
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (sender is DirectSoundOut directSoundOutput)
            {
                directSoundOutput.PlaybackStopped -= OnPlaybackStopped;
                directSoundOutput.Dispose();
                directSoundOutput = null;
            }
        }

        private void PositionTimerTick(object sender, EventArgs e)
        {
            UpdateAudioPosition(
                IsRevertedTrackRun
                    ? _songReader.TotalTime - _songReader.CurrentTime
                    : _songReader.CurrentTime
            );
        }

        private void UpdateAudioPosition(TimeSpan value)
        {
            PositionUpdated?.Invoke(this, AudioPosition = value);
        }

        private IPlayerOutputSettings LoadPlayerOutputSettings()
        {
            if (Directory.Exists(Globals.PlayerPath))
            {
                if (File.Exists(Globals.PlayerOutputSettingsFile))
                {
                    IPlayerOutputSettings settings;
                    using (var file = File.OpenText(Globals.PlayerOutputSettingsFile))
                    {
                        var serializer = new JsonSerializer();
                        settings = serializer.Deserialize(file, typeof(PlayerOutputSettings)) as IPlayerOutputSettings;
                    }

                    return settings;
                }
            }

            return new PlayerOutputSettings();
        }
    }
}

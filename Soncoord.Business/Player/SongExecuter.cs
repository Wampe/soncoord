using NAudio.Extras;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Soncoord.Infrastructure.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Soncoord.Business.Player
{
    public class SongExecuter : IDisposable
    {
        private readonly DispatcherTimer _positionTimer;
        private DirectSoundOut _clickTrackOutput;
        private DirectSoundOut _songTrackOutput;
        private AudioFileReader _clickTrackReader;
        private AudioFileReader _songTrackReader;

        public SongExecuter(ISongSetting songSettings, IPlayerOutputSettings outputSettings)
        {
            _positionTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle)
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };
            _positionTimer.Tick += PositionTimerTick;

            Setup(songSettings, outputSettings);
            UpdateAudioPosition(new TimeSpan(0));
        }

        public event EventHandler<TimeSpan> PositionChanged;
        public event EventHandler<TimeSpan> Started;
        public event EventHandler Stopped;
        public event EventHandler Ended;
        public bool IsTrackRunReverted { get; set; }

        public void Stop()
        {
            _clickTrackOutput.Stop();
            _songTrackOutput.Stop();
        }

        public void Play()
        {
            _positionTimer.Start();

            Parallel.Invoke(() => _clickTrackOutput.Play(), () =>_songTrackOutput.Play()); 

            Started?.Invoke(this, _songTrackReader.TotalTime);
        }

        public void Dispose()
        {
            _positionTimer.Tick -= PositionTimerTick;
            _songTrackOutput.PlaybackStopped -= OnPlaybackStopped;

            _songTrackOutput.Dispose();
            _songTrackOutput = null;

            _clickTrackOutput.Dispose();
            _clickTrackOutput = null;

            _songTrackReader.Dispose();
            _songTrackReader = null;
            
            _clickTrackReader.Dispose();
            _clickTrackReader = null;
        }

        private void Setup(ISongSetting songSetting, IPlayerOutputSettings outputSettings)
        {
            _clickTrackReader = new AudioFileReader(songSetting.ClickTrackPath);
            _clickTrackOutput = new DirectSoundOut(outputSettings.ClickTrackOutputDevice.Guid, 300);
            _clickTrackOutput.Init(_clickTrackReader.ToSampleProvider());

            _songTrackReader = new AudioFileReader(songSetting.MusicTrackPath);
            _songTrackOutput = new DirectSoundOut(outputSettings.SongTrackOutputDevice.Guid, 300);
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
                    outputSettings.EqualizerBands));
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (sender is DirectSoundOut output)
            {
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
        }

        private void PositionTimerTick(object sender, EventArgs e)
        {
            var position = IsTrackRunReverted
                ? _songTrackReader.TotalTime - _songTrackReader.CurrentTime
                : _songTrackReader.CurrentTime;

            UpdateAudioPosition(position);
        }

        private void UpdateAudioPosition(TimeSpan value)
        {
            PositionChanged?.Invoke(this, value);
        }
    }
}

using NAudio.Wave;
using NAudio.WaveFormRenderer;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
//using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;

namespace Soncoord.Audio.Visualizer
{
    public class AudioVisualizerViewModel : BindableBase
    {
        
        public AudioVisualizerViewModel()
        {
            GenerateVisualization();
        }

        private Image _image;
        public Image Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        private Image _image2;
        public Image Image2
        {
            get => _image2;
            set => SetProperty(ref _image2, value);
        }

        private void GenerateVisualization()
        {
            var clickTrackPath = @"";
            var songTrackPath = @"";

            var clickTrackReader = new AudioFileReader(clickTrackPath);
            var songTrackReader = new AudioFileReader(songTrackPath);
            
            //var maxPeakProvider = new MaxPeakProvider();
            var rmsPeakProvider = new RmsPeakProvider(200); // e.g. 200
            //var samplingPeakProvider = new SamplingPeakProvider(200); // e.g. 200
            //var averagePeakProvider = new AveragePeakProvider(4); // e.g. 4

            var clickTrackRendererSettings = new StandardWaveFormRendererSettings
            {
                Width = Convert.ToInt16(clickTrackReader.TotalTime.TotalSeconds)*10,
                TopHeight = 64,
                BottomHeight = 64,

                BackgroundColor = Color.White,
                TopPeakPen = new Pen(Color.DarkGray),
                BottomPeakPen = new Pen(Color.Gray)
            };

            var clickTrackRenderer = new WaveFormRenderer();
            Image = clickTrackRenderer.Render(clickTrackPath, rmsPeakProvider, clickTrackRendererSettings);

            var songTrackRendererSettings = new StandardWaveFormRendererSettings
            {
                Width = Convert.ToInt16(songTrackReader.TotalTime.TotalSeconds)*10,
                TopHeight = 64,
                BottomHeight = 64,

                BackgroundColor = Color.White,
                TopPeakPen = new Pen(Color.DarkGray),
                BottomPeakPen = new Pen(Color.Gray)
            };

            var songTackRenderer = new WaveFormRenderer();
            Image2 = songTackRenderer.Render(songTrackPath, rmsPeakProvider, songTrackRendererSettings);
        }
    }
}

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
            var maxPeakProvider = new MaxPeakProvider();
            var rmsPeakProvider = new RmsPeakProvider(200); // e.g. 200
            var samplingPeakProvider = new SamplingPeakProvider(200); // e.g. 200
            var averagePeakProvider = new AveragePeakProvider(4); // e.g. 4

            var myRendererSettings = new StandardWaveFormRendererSettings();
            myRendererSettings.Width = 800;
            myRendererSettings.TopHeight = 64;
            myRendererSettings.BottomHeight = 64;

            myRendererSettings.BackgroundColor = Color.White;
            myRendererSettings.TopPeakPen = new Pen(Color.DarkGray);
            myRendererSettings.BottomPeakPen = new Pen(Color.Gray);

            var renderer = new WaveFormRenderer();
            var audioFilePath = @"C:\Users\Dennis\Downloads\Tina_Turner_Nutbush_City_Limits(Individuelles_Playback).mp3";

            Image = renderer.Render(audioFilePath, rmsPeakProvider, myRendererSettings);

            renderer = new WaveFormRenderer();
            audioFilePath = @"C:\Users\Dennis\Downloads\Tina_Turner_Nutbush_City_Limits(Klick_Individuelles_Playback) (1).mp3";

            Image2 = renderer.Render(audioFilePath, rmsPeakProvider, myRendererSettings);
        }
    }
}

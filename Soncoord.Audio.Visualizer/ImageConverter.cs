using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Soncoord.Audio.Visualizer
{
    [ValueConversion(typeof(Image), typeof(ImageSource))]
    public class ImageConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var image = value as Image;
            
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            
            var memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Bmp);
            memoryStream.Seek(0, SeekOrigin.Begin);

            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();

            return bitmap;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

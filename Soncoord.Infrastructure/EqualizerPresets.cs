using NAudio.Extras;
using System.Collections.Generic;

namespace Soncoord.Infrastructure
{
    public class EqualizerPresets
    {
        private static readonly EqualizerBand[] Classical = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = -4.875f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = -4.875f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = -4.875f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = -6.375f }
        };
        
        private static readonly EqualizerBand[] Club = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = 1.936f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = 3.484f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = 3.484f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = 3.484f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = 1.936f  },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = 0f }
        };

        private static readonly EqualizerBand[] Dance = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = 5.807f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 4.259f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = 1.162f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = -0.375f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = -0.375f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = -4.125f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = -4.875f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = -4.875f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = -0.375f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = -0.375f }
        };

        private static readonly EqualizerBand[] Flat = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = 0f }
        };

        private static readonly EqualizerBand[] LaptopSpeakerAndHeadphones = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = 2.71f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 6.581f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = 3.097f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = -2.625f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = -1.875f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = 0.775f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = 2.71f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = 5.807f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = 7.742f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = 8.904f }
        };

        private static readonly EqualizerBand[] LargeHall = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = 6.194f},
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 6.194f},
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = 3.484f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = 3.484f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = -3.375f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = -3.375f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = -3.375f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = 0f }
        };

        private static readonly EqualizerBand[] Party = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = 4.259f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 4.259f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = 4.259f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = 4.259f }
        };

        private static readonly EqualizerBand[] Pop = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = -1.5f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 2.71f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = 4.259f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = 4.646f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = 3.097f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = -1.125f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = -1.875f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = -1.875f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = -1.5f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = -1.5f }
        };

        private static readonly EqualizerBand[] Reggae = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = -0.75f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = -4.125f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = 3.871f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = 3.871f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = 0f }
        };

        private static readonly EqualizerBand[] Rock = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = 4.646f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 2.71f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = -3.75f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = -5.25f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = -2.625f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = 2.323f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = 5.42f},
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = 6.581f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = 6.581f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = 6.581f }
        };

        private static readonly EqualizerBand[] Soft = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = 2.71f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 0.775f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = -1.125f},
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = -1.875f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = -1.125f},
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = 2.323f},
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = 5.033f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = 5.807f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = 6.581f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = 7.355f }
        };

        private static readonly EqualizerBand[] Ska = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = -1.875f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = -3.375f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = -3.0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = -0.75f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = 2.323f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = 3.484f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = 5.42f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = 5.807f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = 6.581f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = 5.807f }
        };

        private static readonly EqualizerBand[] FullBass = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = 5.807f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 5.807f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = 5.807f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = 3.484f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = 0.775f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = -3.0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = -5.625f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = -6.75f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = -7.125f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = -7.125f }
        };

        private static readonly EqualizerBand[] SoftRock = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = 2.323f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 2.323f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = 1.162f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = -0.75f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = -3.0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = -3.75f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = -2.625f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = -0.75f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = 1.549f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = 5.42f }
        };

        private static readonly EqualizerBand[] FullTreble = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = -6.375f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = -6.375f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = -6.375f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = -3f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = 1.549f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = 6.581f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = 9.678f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = 9.678f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = 9.678f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = 10.452f }
        };

        private static readonly EqualizerBand[] FullBassAndTreble = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = 4.259f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 3.484f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = -4.875f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = -3.375f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = 0.775f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = 5.033f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = 6.581f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = 7.355f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = 7.355f }
        };

        private static readonly EqualizerBand[] Live = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = -3.375f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = 2.323f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = 3.097f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = 3.484f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = 3.484f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = 2.323f},
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = 1.549f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = 1.549f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = 1.162f }
        };

        private static readonly EqualizerBand[] Techno = new EqualizerBand[]
        {
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 70, Gain = 4.646f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 180, Gain = 3.484f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 320, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 600, Gain = -3.75f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 1000, Gain = -3.375f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 3000, Gain = 0f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 6000, Gain = 4.646f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 12000, Gain = 5.807f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 14000, Gain = 5.807f },
            new EqualizerBand { Bandwidth = 0.8f, Frequency = 16000, Gain = 5.42f }
        };

        public static Dictionary<string, EqualizerBand[]> Presets = new Dictionary<string, EqualizerBand[]>
        {
            { "Classical" , Classical },
            { "Club" , Club },
            { "Dance" , Dance },
            { "Flat" , Flat },
            { "Laptop Speaker & Headphones" , LaptopSpeakerAndHeadphones },
            { "Large Hall" , LargeHall },
            { "Party" , Party },
            { "Pop" , Pop },
            { "Reggae" , Reggae },
            { "Rock" , Rock },
            { "Soft" , Soft },
            { "Ska" , Ska },
            { "Full Bass" , FullBass },
            { "Soft Rock" , SoftRock },
            { "Full Treble" , FullTreble },
            { "Full Bass & Treble" , FullBassAndTreble },
            { "Live" , Live },
            { "Techno" , Techno }
        };
    }
}

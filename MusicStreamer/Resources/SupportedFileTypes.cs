using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicStreamer.Resources
{
    public class SupportedFileTypes
    {
        enum FileTypes { mp3, wma, wav };

        public static string[] ToStringArray()
        {
            return new string[] { FileTypes.mp3.ToString(), FileTypes.wav.ToString(), FileTypes.wma.ToString() };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MusicStreamer.Exceptions
{
    class StreamingInProgressException : System.Exception
    {
        public StreamingInProgressException()
        { }
        public StreamingInProgressException(string massage) : base()
        {
            MessageBox.Show( Message,"Info",MessageBoxButton.OK,MessageBoxImage.Information);
        }
    }
}

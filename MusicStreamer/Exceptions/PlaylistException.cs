using System.Windows;

namespace MusicStreamer.Exceptions
{
    class PlaylistException : System.Exception
    {
        public PlaylistException(string message)
            : base(message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}

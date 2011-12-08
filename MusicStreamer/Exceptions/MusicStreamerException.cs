using System.Windows.Forms;

namespace MusicStreamer.Exceptions
{
    class MusicStreamerException :  System.Exception
    {

        public MusicStreamerException()
        {
        }
        public MusicStreamerException(string message) : base(message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

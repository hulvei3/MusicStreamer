using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMPLib;
using System.ComponentModel;
using System.Windows.Data;

namespace MusicStreamer.Models
{
    class PlayerEngineModel
    {
        private WindowsMediaPlayer _mediaPlayer;

        public WindowsMediaPlayer MediaPlayer
        {
            get { return _mediaPlayer; }
        }

        public PlayerEngineModel(bool shouldDo_AutoStart)
        {
            _mediaPlayer = new WindowsMediaPlayer();
            _mediaPlayer.settings.autoStart = shouldDo_AutoStart;
        }
        
            

    }
}

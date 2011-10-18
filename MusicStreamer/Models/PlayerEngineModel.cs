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

        public PlayerEngineModel(bool shouldDo_AutoStart, bool show_WMPErrors)
        {
            _mediaPlayer = new WindowsMediaPlayer();

            // should wmp.dll start playback when the url is set
            _mediaPlayer.settings.autoStart = shouldDo_AutoStart;

            // should wmp.dll show errors as dialogs or keep silent
            _mediaPlayer.settings.enableErrorDialogs = show_WMPErrors;
        }
        
            

    }
}

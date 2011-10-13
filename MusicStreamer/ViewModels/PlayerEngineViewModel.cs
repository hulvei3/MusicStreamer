using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MusicStreamer.Models;
using MusicStreamer.Exceptions;

namespace MusicStreamer.ViewModels
{
    class PlayerEngineViewModel : PropertyAndErrorHandler
    {
        // backing fields
        private WMPLib.IWMPSettings _settings;

        // constructor
        public PlayerEngineViewModel(PlayerEngineModel player)
        {
            _settings = player.MediaPlayer.settings;
        }

        // properties (ICommands)
        public int Volume
        {
            get;
            set;
        }
        public int Balance
        {
            get;
            set;
        }


        // properties


    }
}

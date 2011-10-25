using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using MusicStreamer.Models;
using MusicStreamer.Exceptions;

namespace MusicStreamer.ViewModels
{
    // service class for volume and balance settings. Also provides an media-error handler
    class PlayerEngineViewModel : MusicStreamer.Exceptions.PropertyAndErrorHandler
    {

        // backing fields
        private WMPLib.IWMPSettings _settings;

        // constructors
        public PlayerEngineViewModel()
        { }
        public PlayerEngineViewModel(PlayerEngineModel player)
        {
            _settings = player.MediaPlayer.settings;

            player.MediaPlayer.MediaError += new WMPLib._WMPOCXEvents_MediaErrorEventHandler(MediaPlayer_MediaError);

        }
        // eventhandler for media-errors
        void MediaPlayer_MediaError(object pMediaObject)
        {
            DebugText = "ERROR: " + pMediaObject.ToString();
            throw new NotImplementedException();
        }

        // properties
        public double Volume
        {
            get { return Convert.ToDouble(_settings.volume); }
            set
            {
                _settings.volume = Convert.ToInt32(value);
                OnPropertyChanged("Volume");
            }
        }
        public int Balance
        {
            get;
            set;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

using MusicStreamer.Models;

namespace MusicStreamer.ViewModels
{
    class CurrentSongViewModel : MusicStreamer.Exceptions.PropertyAndErrorHandler
    {

        // backing fields
        private string _currentSongUrl;
        private WMPLib.IWMPControls _controls;
        private WMPLib.IWMPMedia _currentMedia;

        //contructor
        public CurrentSongViewModel(PlayerEngineModel playerModel)
        {
            Controls = playerModel.MediaPlayer.controls;
            CurrentMedia = playerModel.MediaPlayer.currentMedia;

            PlayCommand = new MusicStreamer.CustomCommands.PlayCommand(this);
            StopCommand = new CustomCommands.StopCommand(this);
        }

        // properties (ICommands)
        public ICommand PlayCommand
        {
            get;
            set;
        }
        public ICommand StopCommand
        {
            get;
            set;
        }

        // properties
        public String CurrentSongUrl
        {
            get;
            set;
        }
        public WMPLib.IWMPControls Controls 
        { get; set; }
        public WMPLib.IWMPMedia CurrentMedia 
        { get; set; }


        // gets remaining time of current song (in seconds)
        public double RemainingTime
        {
            get
            {
                return Math.Floor(CurrentMedia.duration - Controls.currentPosition);
            }
        }



        // gets/sets current duration of current song (in seconds)
        public double CurrentTime
        {
            get
            {
                return Controls.currentPosition;
            }
            set
            {
                double newPosition = Convert.ToDouble(value);

                // checks if new position is within limit of current media duration
                Controls.currentPosition = newPosition <= CurrentMedia.duration ? newPosition : Controls.currentPosition;
                OnPropertyChanged("CurrentTime");
                
            }

        }


        // ICommand implementations..

        internal void PlayCurrentSong()
        {
            _controls.play();
        }
        internal void PlayCurrentSong(String url)
        {
            CurrentSongUrl = url;
            _controls.play();
        }
        internal void StopCurrentSong()
        {
            _controls.stop();
            // maybe also close the player to release resources ??
        }
        internal void PauseCurrentSong()
        {
            _controls.pause();
        }


    }
}

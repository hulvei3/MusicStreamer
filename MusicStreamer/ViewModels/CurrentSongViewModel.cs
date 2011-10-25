using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

using MusicStreamer.Models;
using MusicStreamer.CustomCommands;

namespace MusicStreamer.ViewModels
{
    class CurrentSongViewModel : MusicStreamer.Exceptions.PropertyAndErrorHandler
    {

        // backing fields

        //private String _currentSongUrl;
        private WMPLib.IWMPControls _controls;
        private WMPLib.IWMPMedia _currentMedia;
        private WMPLib.WindowsMediaPlayer _player;

        //contructor
        public CurrentSongViewModel(PlayerEngineModel playerModel)
        {
            _controls = playerModel.MediaPlayer.controls;
            _currentMedia = playerModel.MediaPlayer.currentMedia;
            _player = playerModel.MediaPlayer;

            PlayCommand = new PlayCommand(this);
            PauseCommand = new PauseCommand(this);
            StopCommand = new StopCommand(this);
            
        }

        // properties (ICommands)
        public ICommand PlayCommand
        {
            get;
            set;
        }
        public ICommand PauseCommand
        { 
            get;
            set; 
        }
        public ICommand StopCommand
        {
            get;
            set;
        }

        // PROPERTIES

        // current url set in player (can be empty)
        public String Url
        {
            get { return _player.URL; }
            set
            {
                _player.URL = value;
                OnPropertyChanged("Url");
            }
        }


        // remaining time of current song (in seconds)
        public double RemainingTime
        {
            get
            {
                return Math.Floor(_currentMedia.duration - _controls.currentPosition);
            }
        }



        // gets/sets current duration of current song (in seconds)
        public double CurrentTime
        {
            get
            {
                return _controls.currentPosition;
            }
            set
            {
                double newPosition = Convert.ToDouble(value);

                // checks if new position is within limit of current media duration
                _controls.currentPosition = newPosition <= _currentMedia.duration ? newPosition : _controls.currentPosition;
                OnPropertyChanged("CurrentTime");
                
            }

        }


        // ICommand implementations..

        internal void PlayCurrentSong()
        {
            _controls.play();
            DebugText = "Playing..";

            new System.Threading.Thread(new System.Threading.ThreadStart(RunTimeService)).Start();

        }
        internal void PlayCurrentSong(String url)
        {
            Url = url;
            PlayCurrentSong();
        }
        internal void StopCurrentSong()
        {
            _controls.stop();
            DebugText = "Stopping";
            // maybe also close the player to release resources ??
        }
        internal void PauseCurrentSong()
        {
            _controls.pause();
            DebugText = "Player paused";
        }

        // seperate thread for updating time counter
        private void RunTimeService()
        {
            System.Threading.Thread.CurrentThread.Name = "Time Updater Service Thread";
            
            // sets new thread to be a background-thread, so it stops when application closes.
            // If not this thread whould keep the app running, though it is closing..
            System.Threading.Thread.CurrentThread.IsBackground = true;


            // notifies the View every second that the "CurrentTime"-property has been updated (should pause together with the player??)
            while (true)
            {
                OnPropertyChanged("CurrentTime");
                System.Threading.Thread.Yield();
                System.Threading.Thread.Sleep(1000);
            }
        }

    }
}

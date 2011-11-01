using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

using MusicStreamer.Models;
using MusicStreamer.CustomCommands;
using System.Windows;

namespace MusicStreamer.ViewModels
{
    class CurrentSongViewModel : MusicStreamer.Exceptions.PropertyAndErrorHandler
    {

        // backing fields

        //private String _currentSongUrl;
        private WMPLib.IWMPControls _controls;
        //private WMPLib.IWMPMedia _currentMedia;
        private WMPLib.WindowsMediaPlayer _player;

        //contructor
        public CurrentSongViewModel(PlayerEngineModel playerModel)
        {
            _player = playerModel.MediaPlayer;
            _controls = _player.controls;

            IsPlaying = false;
            HasPlayed = false;

            PlayPauseCommand = new PlayPauseCommand(this);
            StopCommand = new StopCommand(this);
            
        }

        // properties (ICommands)
        public ICommand PlayPauseCommand
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


        // remaining time of current song (in seconds with decimals)
        public double RemainingTime
        {
            get
            {
                return Math.Floor(CurrentMedia.duration - _controls.currentPosition);
            }
        }



        // gets/sets current duration of current song (in seconds)
        public TimeSpan CurrentTimeFormatted
        {
            get
            {
                return TimeSpan.FromSeconds(Math.Floor(_controls.currentPosition));
            }
        }

        public double CurrentTime 
        {
            get { return _controls.currentPosition; }
            set
            {
                double newPosition = Convert.ToDouble(value);

                // checks if new position is within limit of current media duration
                _controls.currentPosition = newPosition <= CurrentMedia.duration ? newPosition : _controls.currentPosition;
                
                // maybe this if media is paused??
                //OnPropertyChanged("CurrentTime");
            }
        }

        public double SongLength
        {
            // null error (_currentMedia)
            get { return CurrentMedia == null ? 0 : CurrentMedia.duration; }
        }

        // private/internal Properties

        private WMPLib.IWMPMedia CurrentMedia
        {
            get;
            set;
        }

        private System.Threading.Thread TimeUpdaterThread
        { 
            get; 
            set; 
        }

        public bool IsPlaying
        {
            get;
            set;
        }

        public bool HasPlayed
        {
            get;
            set;
        }

        // ICommand implementations..

        internal void PlayCurrentSong()
        {

            // starting time updater thread
            TimeUpdaterThread = new System.Threading.Thread(new System.Threading.ThreadStart(RunTimeService));
            TimeUpdaterThread.Start();

            

            // plays content from 'Url'
            _controls.play();
            IsPlaying = true;
            DebugText = "Playing..";
        }
        internal void PlayCurrentSong(String url)
        {

            Url = url;
            // TODO make method prepareSong(string url) that load url (and thereby CurrentMedia) into the player before playing
            // updating CurrentMedia reference, (for updating songlength etc.)
            CurrentMedia = _player.currentMedia;

            PlayCurrentSong();
        }
        internal void StopCurrentSong()
        {
            _controls.stop();
            DebugText = "Stopping";

            // stop and remove timer-updater-thread
            TimeUpdaterThread.Abort();
            TimeUpdaterThread = null;

            // maybe also close the player to release resources ??
        }
        internal void PauseCurrentSong()
        {
            _controls.pause();
            DebugText = "Player paused";
            HasPlayed = true;
            IsPlaying = false;
            // stop/pause timer-updater-thread
        }


        // seperate thread for updating time counter
        private void RunTimeService()
        {
            bool isSonglengthSet = false;

            System.Threading.Thread.CurrentThread.Name = "Time Updater Service Thread";
            
            // sets new thread to be a background-thread, so it stops when application closes.
            // If not this thread whould keep the app running, though it is closing..
            System.Threading.Thread.CurrentThread.IsBackground = true;

            // notifies the View that time-properties has been updated (terminates when player is stopped)
            while (true)
            {
                OnPropertyChanged("CurrentTime");
                OnPropertyChanged("CurrentTimeFormatted");

                //TODO should only update this once
                if (!isSonglengthSet)
                    OnPropertyChanged("SongLength");

                System.Threading.Thread.Yield();
                System.Threading.Thread.Sleep(200);

                
                

                // test
                //DebugText = TimeSpan.FromSeconds(Math.Floor(CurrentTime)).ToString();
                    
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
<<<<<<< HEAD
<<<<<<< HEAD
using StreamerLib;
//using MusicStreamer.Models;
using MusicStreamer.CustomCommands;
=======

using MusicStreamer.Models;
>>>>>>> 7d36a33e31396d69709d175f831a2b8b96fdc731
=======
using StreamerLib;
//using MusicStreamer.Models;
using MusicStreamer.CustomCommands;

>>>>>>> 47fd055002a810fb8f1b8379868f111a6d56ba35
using System.Windows;
using System.IO;
using MusicStreamer.ViewModels.Streamer;


namespace MusicStreamer.ViewModels.Player
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
            init();
        }

        private void init()
        {
            _controls = _player.controls;
            //handle er nede i bunden
            _player.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(_player_PlayStateChange);

            Streamer = new StreamerViewModel();
            StreamerDownloadProgress = -1;
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
                OnPropertyChanged("CurrentTime");
            }
        }

        public double SongLength
        {
            // null error (_currentMedia)
            get { return CurrentMedia == null ? 0 : CurrentMedia.duration; }
        }

        public StreamerViewModel Streamer { get; set; }

        public int StreamerDownloadProgress { get; set; }

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

        public WMPLib.WMPPlayState PlayerState
        {
            get { return _player.playState; }
        }

        internal void PlayCurrentSong()
        {
            if (_player.URL == null) return;

            // starting time updater thread
            TimeUpdaterThread = new System.Threading.Thread(new System.Threading.ThreadStart(RunTimeService));
            TimeUpdaterThread.Start();

            // plays content from 'Url'
            _controls.play();
            DebugText = "Playing..";
        }
        internal void PlayCurrentSong(String url)
        {
            // start prepare service
            // parse remote-url to local-url (temp)
            string localurl = PrepareSongService(url);

            Url = localurl;
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
            if (TimeUpdaterThread != null)
                TimeUpdaterThread.Abort();
            TimeUpdaterThread = null;

            // maybe also close the player to release resources ??
        }
        internal void PauseCurrentSong()
        {
            _controls.pause();
            DebugText = "Player paused";
            // stop/pause timer-updater-thread
        }

        internal void NextSongInPlaylist()
        {
            _controls.next();
        }

        internal void PreviousSongInPlaylist()
        {
            _controls.previous();
        }

        // seperate thread for preparing downloaded track (not downloading itself)
        private string PrepareSongService(string remoteUrl)
        {
            //remote-to-local url parsing
            // TODO exceptions

            var streamer = new StreamerViewModel();
            streamer.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(streamer_DownloadProgressChanged);

            var filename = streamer.StreamMedia(remoteUrl);

            FileInfo file = new FileInfo(filename);
            //TODO loading indicator here..
            while (File.Exists(filename) && filename != null)
            {
                if (file.Length > 300000)
                    break;
                file.Refresh();
            }
            //TODO loading indicator remove here..

            return filename;
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

        void streamer_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            int kbytes = (int)(e.BytesReceived / 1024);

            StreamerDownloadProgress = kbytes;

            OnPropertyChanged("StreamerDownloadProgress");
        }

        void _player_PlayStateChange(int NewState)
        {
            OnPropertyChanged("PlayerState");
        }


        
    }
}

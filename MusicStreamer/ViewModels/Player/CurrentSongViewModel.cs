using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using StreamerLib;
using System.Windows;
using System.IO;
using MusicStreamer.ViewModels.Streamer;
using MusicStreamer.ViewModels.Playlist;
using WMPLib;
using MusicStreamer.Exceptions;


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
            Streaming = false;
        }

        public void SetupStreamer()
        {
            Streamer = new StreamerViewModel();
            Streamer.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(streamer_DownloadProgressChanged);
            Streamer.DownloadCompleted += new AsyncCompletedEventHandler(streamer_DownloadCompleted);
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
                return Math.Floor(_player.currentMedia.duration - _controls.currentPosition);
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
                //_controls.currentPosition = newPosition <= SongLength ? newPosition : _controls.currentPosition;

                _controls.currentPosition = newPosition;

                // maybe this if media is paused??
                
            }
        }

        public double SongLength
        {
            get;
            set;
        }

        //public double SongLength
        //{
        //    // null error (_currentMedia)
        //    get { return _player.currentMedia == null ? 0 : _player.currentMedia.duration; }
        //}

        public StreamerViewModel Streamer { get; set; }

        public double StreamerDownloadProgress { get; set; }

        // private/internal Properties

        public FileInfo CurrentFile
        {
            get;
            set;
        }

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
        internal void PlayCurrentSong(PlaylistItemViewModel song)
        {


            // start prepare service
            // parse remote-url to local-url (temp)
            string localurl = PrepareSongService(song.Url);
            if (localurl == null) return;

            Url = localurl;
            
            //CurrentMedia = _player.currentMedia;

            PlayCurrentSong();

            // set 'playing' in playlist (used for next/prev)
            MainWindowViewModel.Instance.Playlist.Playing = song;
        }
        internal void StopCurrentSong()
        {
            _controls.stop();
            DebugText = "Stopping";

            // stop and remove timer-updater-thread
            if (TimeUpdaterThread != null)
                TimeUpdaterThread.Abort();
            TimeUpdaterThread = null;

            // TODO removes temp-files

        }
        internal void PauseCurrentSong()
        {
            _controls.pause();
            DebugText = "Player paused";
            // stop/pause timer-updater-thread
        }
        internal void UnPause()
        {
            _controls.play();
            DebugText = "Playing...";
        }

        internal void NextSongInPlaylist()
        {

            // next song in playlist
            var next = MainWindowViewModel.Instance.Playlist.GetNextSong();

            StopCurrentSong();

            if (next != null)
            {
                PlayCurrentSong(next);
                
            }

            // wmp.dll
            //_controls.next();
        }

        internal void PreviousSongInPlaylist()
        {
            // previous song in playlist
            var prev = MainWindowViewModel.Instance.Playlist.GetPreviousSong();

            StopCurrentSong();

            if (prev != null)
            {
                PlayCurrentSong(prev);
            }

            // wmp.dll
            //_controls.previous();
        }

        // seperate thread for preparing downloaded track (not downloading itself)
        private string PrepareSongService(string remoteUrl)
        {

            //remote-to-local url parsing
            var filename = Streamer.StreamMedia(remoteUrl);

            if (filename == null) return null;

            Streaming = true;

            CurrentFile = new FileInfo(filename);
            //TODO loading indicator here..
            while (File.Exists(filename) && filename != null)
            {
                if (CurrentFile.Length > 300000)
                    break;
                CurrentFile.Refresh();
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

                OnPropertyChanged("SongLength");
                

                System.Threading.Thread.Yield();
                System.Threading.Thread.Sleep(200);

                
                

                // test
                //DebugText = TimeSpan.FromSeconds(Math.Floor(CurrentTime)).ToString();
                    
            }
        }

        void streamer_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            

            double kbytes = e.BytesReceived / 1024;

            StreamerDownloadProgress = kbytes;
            
            

            OnPropertyChanged("StreamerDownloadProgress");
        }

        void streamer_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {


            
            Streaming = false;
            //MainWindowViewModel.Instance.Playlist.ShowFileInfo(_player.currentMedia);

            SongLength = _player.newMedia(CurrentFile.Name).duration;
            OnPropertyChanged("SongLength");

            // update CurrentMedia
            //CurrentMedia = _player.currentMedia;
        }

        private bool _streaming;

        public bool Streaming
        {
            get { return _streaming; }
            set { _streaming = value; OnPropertyChanged("Streaming"); }
        }


        void _player_PlayStateChange(int NewState)
        {
            OnPropertyChanged("PlayerState");
        }


        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using MusicStreamer.CustomCommands;
using MusicStreamer.ViewModels.Server;
using System.Windows;
using MusicStreamer.ViewModels.Player;
using MusicStreamer.ViewModels.Playlist;
using StreamerLib;
using MusicStreamer.Util;

namespace MusicStreamer.ViewModels
{
    class MainWindowViewModel : MusicStreamer.Exceptions.PropertyAndErrorHandler
    {
        private static MainWindowViewModel _this;

        //backing fields
        private PlaylistViewModel _playlist;
        private CurrentSongViewModel _currentSong;
        private PlayerEngineViewModel _player;
        private Window _mainUI;

        // properties
        public CommandLibrary CommandLib { get; set; }

        public static MainWindowViewModel Instance { get { return _this; } }

        public CurrentSongViewModel CurrentSong
        {
            get { return _currentSong; }
            set
            { 
                _currentSong = value;
                OnPropertyChanged("CurrentSong");
            } 
        }
        public PlaylistViewModel Playlist
        {
            get { return _playlist; }
            set
            {
                _playlist = value;
                OnPropertyChanged("Playlist");
            }
        }
        // not sure we need this
        public PlayerEngineViewModel Player 
        {
            get { return _player; } 
            set { _player = value; } 
        }

        public Server.ServerNavigationViewModel Navigation { get; set; }

        public ConnectionLibrary Servers { get; set; }


        public MainWindowViewModel()
        {
            // init instance of wmp-player from wmp.dll
            PlayerEngineModel playerEngine = new PlayerEngineModel(false,false);

            // init view-models taking wmp-player as argument
            Player = new PlayerEngineViewModel(playerEngine);
            CurrentSong = new CurrentSongViewModel(playerEngine);
            Playlist = new PlaylistViewModel(playerEngine);
            Navigation = new Server.ServerNavigationViewModel();
            Servers = new ConnectionLibrary();

            // undo/redo
            CommandLib = new CommandLibrary();

            Player.Volume = 50;

            _this = this;
        }










        
    }
}

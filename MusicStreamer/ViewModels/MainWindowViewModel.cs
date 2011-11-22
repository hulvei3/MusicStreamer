using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using MusicStreamer.Models;
using MusicStreamer.CustomCommands;
using MusicStreamer.ViewModels.Server;
using MusicStreamer.Models.Server;
using System.Windows;

namespace MusicStreamer.ViewModels
{
    class MainWindowViewModel : MusicStreamer.Exceptions.PropertyAndErrorHandler
    {

        //MusicStreamer.Exceptions.PropertyAndErrorHandler

        //backing fields
        private PlaylistViewModel _playlist;
        private CurrentSongViewModel _currentSong;
        private PlayerEngineViewModel _player;



        // properties
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

        public Server.ServerNavigationViewModel Navigation 
        { 
            get; 
            set; 
        }
        
        public ICommand ConnectCommand
        {
            get;
            set;
        }

        
            
        public MainWindowViewModel()
        {

            PlayerEngineModel playerEngine = new PlayerEngineModel(false,false);

            Player = new PlayerEngineViewModel(playerEngine);
            CurrentSong = new CurrentSongViewModel(playerEngine);
            Playlist = new PlaylistViewModel(playerEngine);

            //Smides i egen metode, som ConnectCommand bruger
            //Models.Server.ServerConnectionModel serverConnection = new Models.Server.ServerConnectionModel();
            
            
            Navigation = new Server.ServerNavigationViewModel();

            ConnectCommand = new ConnectCommand(this);

            Player.Volume = 50;            

            // FTPservice should startup here
            // other startups ?

            
        }

        






        
    }
}

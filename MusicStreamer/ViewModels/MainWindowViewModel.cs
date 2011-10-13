using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;

using MusicStreamer.Models;

namespace MusicStreamer.ViewModels
{
    class MainWindowViewModel : MusicStreamer.Exceptions.PropertyAndErrorHandler
    {

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
        

        public MainWindowViewModel()
        {

            PlayerEngineModel playerEngine = new PlayerEngineModel(false);

            Player = new PlayerEngineViewModel(playerEngine);
            CurrentSong = new CurrentSongViewModel(playerEngine);
            Playlist = new PlaylistViewModel(playerEngine);
            

            


            // FTPservice should startup here
            // other startups ?

            
            
        }

        



    }
}

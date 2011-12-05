using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;
using System.Windows;
using MusicStreamer.ViewModels.Playlist;

namespace MusicStreamer.CustomCommands
{
    class AddToPlaylistCommand : ICommand
    {
        //private PlaylistViewModel _plwm;
        private MainWindowViewModel _mwvm;
        
        public AddToPlaylistCommand(MainWindowViewModel mwvm)
        {
            //this._plwm = plwm;
            this._mwvm = mwvm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            
            String url = _mwvm.Navigation.CurrentLocation + (String) parameter;
            PlaylistItemViewModel playlistItem = new PlaylistItemViewModel(url);
            _mwvm.Playlist.AddToPlaylist(playlistItem);
        }
    }
}

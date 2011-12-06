using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;
using MusicStreamer.ViewModels.Playlist;
using System.Windows.Forms;

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
             String url = _mwvm.Navigation.CurrentLocation + (String)parameter;
             PlaylistItemViewModel playlistItem = new PlaylistItemViewModel(url);
            if(parameter.ToString().EndsWith(".mp3"))
            {
                _mwvm.Playlist.AddToPlaylist(playlistItem);
            }
            else MessageBox.Show("Not able to add this file/folder!", "Critical Warning", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);   
 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;
using System.Windows;
using MusicStreamer.Interfaces;

namespace MusicStreamer.CustomCommands
{
    class SavePlaylistCommand : IStreamerCommand
    {
        private PlaylistViewModel _plvm;

        public SavePlaylistCommand(PlaylistViewModel plvm)
        {
            this._plvm = plvm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _plvm.SavePlaylist();
            MessageBox.Show("Playlist saved!");
        }

        public void Execute()
        {
            Execute(null);
        }

        public void UnExecute()
        {
            throw new NotImplementedException();
        }
    }
}

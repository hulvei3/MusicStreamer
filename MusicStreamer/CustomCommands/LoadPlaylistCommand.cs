using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;
using System.Windows.Forms;

namespace MusicStreamer.CustomCommands
{
    class LoadPlaylistCommand : ICommand
    {
        private PlaylistViewModel _plvm;

        public LoadPlaylistCommand(PlaylistViewModel plvm)
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
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "NAS Streamer Playlist (*.nsp)|*.nsp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                _plvm.LoadPlaylist(open.FileName.ToString());
            }
        }
    }
}

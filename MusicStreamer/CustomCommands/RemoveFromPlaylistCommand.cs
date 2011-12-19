using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicStreamer.Interfaces;
using MusicStreamer.ViewModels;
using MusicStreamer.ViewModels.Playlist;
using StreamerLib;

namespace MusicStreamer.CustomCommands
{
    class RemoveFromPlaylistCommand : IBaseCommand
    {
        private MainWindowViewModel _mwvm;

        public RemoveFromPlaylistCommand(MainWindowViewModel mvvm)
        {
            _mwvm = mvvm;
        }

        public void Execute(object parameter)
        {
            PlaylistItemModel item = (PlaylistItemModel)parameter;

            _mwvm.Playlist.RemoveFromPLaylist(item);

            _playlistItem = item;
        }

        PlaylistItemModel _playlistItem;

        public void UnExecute()
        {
            _mwvm.Playlist.AddToPlaylist(_playlistItem);
        }

        #region Unused members

        public void Execute()
        {
            Execute(_playlistItem);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        #endregion
    }
}

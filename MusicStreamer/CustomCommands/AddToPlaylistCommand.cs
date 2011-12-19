using System;
using MusicStreamer.ViewModels;
using MusicStreamer.ViewModels.Playlist;
using MusicStreamer.Interfaces;
using MusicStreamer.Exceptions;
using MusicStreamer.Controls;

namespace MusicStreamer.CustomCommands
{
    class AddToPlaylistCommand : IBaseCommand
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
            //// saves parameter to be used, if redo is invoked
            //RedoContext = parameter;

            String url = _mwvm.Navigation.CurrentLocation + (String)parameter;
            PlaylistItemViewModel playlistItem = new PlaylistItemViewModel(url);
            playlistItem.RemoveCommand = _mwvm.CommandLib.RemoveFromPlaylistCommand;

            bool isNotSupported = false;
            foreach (var type in SupportedFileTypes.ToStringArray())
            {
                if (!parameter.ToString().EndsWith(type))
                {
                    isNotSupported = true;
                }
                else
                {
                    _mwvm.Playlist.AddToPlaylist(playlistItem);
                    isNotSupported = false;
                    break;
                }
            }
            if (isNotSupported)
                throw new PlaylistException("Not able to add this file!\nFileformat not supported.");

            _playlistItem = playlistItem;
        }

        public void Execute()
        {
            Execute(_playlistItem.Url);
        }

        private PlaylistItemViewModel _playlistItem;

        public void UnExecute()
        {
            _mwvm.Playlist.RemoveFromPLaylist(_playlistItem);
        }
    }
}

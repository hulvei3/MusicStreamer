﻿using System;
using System.Linq;
using MusicStreamer.Interfaces;
using MusicStreamer.ViewModels;
using StreamerLib;

namespace MusicStreamer.CustomCommands
{
    class PlayPauseCommand : IBaseCommand
    {
        private MainWindowViewModel _vm;

        public PlayPauseCommand(MainWindowViewModel vm)
        {
            _vm = vm;
        }

        public void Execute(object parameter)
        {

            var song = _vm.Playlist.SelectedPlaylistItem;
            if (song == null)
            {
                try
                {
                    song = _vm.Playlist.CurrentUIPlaylist.ElementAt(0);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    return;
                }
            }
            var url = song.Url;

            // parameter comes from the view (textBoxCurrentSong.Content)
            switch (_vm.CurrentSong.PlayerState)
            {
                case WMPLib.WMPPlayState.wmppsBuffering:
                    break;
                case WMPLib.WMPPlayState.wmppsLast:
                    break;
                case WMPLib.WMPPlayState.wmppsMediaEnded:
                    break;
                case WMPLib.WMPPlayState.wmppsPaused:
                    _vm.CurrentSong.UnPauseCurrentSong();
                    break;
                case WMPLib.WMPPlayState.wmppsPlaying:
                    _vm.CurrentSong.PauseCurrentSong();
                    break;
                case WMPLib.WMPPlayState.wmppsReady:
                    //play new song passing the new url
                    DoIt(song);
                    break;
                case WMPLib.WMPPlayState.wmppsReconnecting:
                    break;
                case WMPLib.WMPPlayState.wmppsScanForward:
                    break;
                case WMPLib.WMPPlayState.wmppsScanReverse:
                    break;
                case WMPLib.WMPPlayState.wmppsStopped:
                    DoIt(song);
                    break;
                case WMPLib.WMPPlayState.wmppsTransitioning:
                    break;
                case WMPLib.WMPPlayState.wmppsUndefined:
                    DoIt(song);
                    break;
                case WMPLib.WMPPlayState.wmppsWaiting:
                    break;
                default:
                    break;
            }
        }

        private void DoIt(PlaylistItemModel song)
        {
            new Action(() =>
                {
                    _vm.CurrentSong.BeginStreaming(song);
                }).BeginInvoke(null, null);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void UnExecute()
        {
            throw new NotImplementedException();
        }
    }
}

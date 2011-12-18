﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;
using MusicStreamer.Interfaces;
using MusicStreamer.ViewModels.Player;
using MusicStreamer.Exceptions;
using MusicStreamer.ViewModels.Playlist;

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
            //string url = (string)parameter;

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
                    //try
                    //{
                    //    throw new PlaylistException("Playlist is empty!\n\nConnect to a server and add songs to the playlist, or load one.");
                    //}
                    //catch (PlaylistException ex) { return; }
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

        private void DoIt(PlaylistItemViewModel song)
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

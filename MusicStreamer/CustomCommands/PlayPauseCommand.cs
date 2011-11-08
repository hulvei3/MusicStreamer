using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;

namespace MusicStreamer.CustomCommands
{
    class PlayPauseCommand : ICommand
    {
        private readonly CurrentSongViewModel _vm;

        public PlayPauseCommand(CurrentSongViewModel vm)
        {
            _vm = vm;
        }

        public void Execute(object parameter)
        {
            // parameter comes from the view (textBoxCurrentSong.Content)
            switch (_vm.PlayerState)
            {
                case WMPLib.WMPPlayState.wmppsBuffering:
                    break;
                case WMPLib.WMPPlayState.wmppsLast:
                    break;
                case WMPLib.WMPPlayState.wmppsMediaEnded:
                    break;
                case WMPLib.WMPPlayState.wmppsPaused:
                    _vm.PlayCurrentSong();
                    break;
                case WMPLib.WMPPlayState.wmppsPlaying:
                    _vm.PauseCurrentSong();
                    break;
                case WMPLib.WMPPlayState.wmppsReady:
                    //play new song passing the new url
                    _vm.PlayCurrentSong((string)parameter);
                    break;
                case WMPLib.WMPPlayState.wmppsReconnecting:
                    break;
                case WMPLib.WMPPlayState.wmppsScanForward:
                    break;
                case WMPLib.WMPPlayState.wmppsScanReverse:
                    break;
                case WMPLib.WMPPlayState.wmppsStopped:
                    _vm.PlayCurrentSong();
                    break;
                case WMPLib.WMPPlayState.wmppsTransitioning:
                    break;
                case WMPLib.WMPPlayState.wmppsUndefined:
                    _vm.PlayCurrentSong((string)parameter);
                    break;
                case WMPLib.WMPPlayState.wmppsWaiting:
                    break;
                default:
                    break;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}

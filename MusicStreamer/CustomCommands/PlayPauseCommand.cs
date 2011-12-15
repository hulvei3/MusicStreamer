using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;
using MusicStreamer.Interfaces;
using MusicStreamer.ViewModels.Player;

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
            string url = (string)parameter;

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
                    _vm.CurrentSong.PlayCurrentSong();
                    break;
                case WMPLib.WMPPlayState.wmppsPlaying:
                    _vm.CurrentSong.PauseCurrentSong();
                    break;
                case WMPLib.WMPPlayState.wmppsReady:
                    //play new song passing the new url
                    _vm.CurrentSong.PlayCurrentSong((string)parameter);
                    break;
                case WMPLib.WMPPlayState.wmppsReconnecting:
                    break;
                case WMPLib.WMPPlayState.wmppsScanForward:
                    break;
                case WMPLib.WMPPlayState.wmppsScanReverse:
                    break;
                case WMPLib.WMPPlayState.wmppsStopped:
                    _vm.CurrentSong.PlayCurrentSong((string)parameter);
                    break;
                case WMPLib.WMPPlayState.wmppsTransitioning:
                    break;
                case WMPLib.WMPPlayState.wmppsUndefined:
                    if (url == "") return;
                    _vm.CurrentSong.PlayCurrentSong((string)parameter);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;
using MusicStreamer.ViewModels.Player;
using MusicStreamer.Interfaces;

namespace MusicStreamer.CustomCommands
{
    class StopCommand : IBaseCommand
    {
        private readonly CurrentSongViewModel _vm;

        public StopCommand(CurrentSongViewModel vm)
        {
            _vm = vm;
        }

        public void Execute(object parameter)
        {
            if (_vm.PlayerState != WMPLib.WMPPlayState.wmppsUndefined)
                _vm.StopCurrentSong();
        }
        public bool CanExecute(object parameter)
        {
            // stopping an extra time can't hurt
            return true;
        }

        public event EventHandler CanExecuteChanged;


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

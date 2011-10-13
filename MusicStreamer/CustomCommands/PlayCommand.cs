using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;

namespace MusicStreamer.CustomCommands
{
    class PlayCommand : ICommand
    {
        private readonly CurrentSongViewModel _vm;

        public PlayCommand(CurrentSongViewModel vm)
        {
            _vm = vm;
        }

        public void Execute(object parameter)
        {
            _vm.PlayCurrentSong();
        }


        public bool CanExecute(object parameter)
        {
            return _vm.CurrentSongUrl == null;
        }

        public event EventHandler CanExecuteChanged;
    }
}

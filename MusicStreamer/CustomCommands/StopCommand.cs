using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;

namespace MusicStreamer.CustomCommands
{
    class StopCommand : ICommand
    {
        private readonly CurrentSongViewModel _vm;

        public StopCommand(CurrentSongViewModel vm)
        {
            _vm = vm;
        }

        public void Execute(object parameter)
        {
            _vm.StopCurrentSong();
        }
        public bool CanExecute(object parameter)
        {
            // stopping an extra time doesn't hurt
            return true;
        }

        public event EventHandler CanExecuteChanged;

        
    }
}

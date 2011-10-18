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
            //_vm.PlayCurrentSong(@"C:\Users\Morten Hulvej\Desktop\07 I Remember.mp3");
            _vm.PlayCurrentSong((string)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}

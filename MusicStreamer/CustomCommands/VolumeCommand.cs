using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;

namespace MusicStreamer.CustomCommands
{
    class VolumeCommand : ICommand
    {
        private readonly PlayerEngineViewModel _vm;

        public VolumeCommand(PlayerEngineViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

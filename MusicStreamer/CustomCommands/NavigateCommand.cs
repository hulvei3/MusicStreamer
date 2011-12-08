using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;
using System.Windows;
using MusicStreamer.Interfaces;

namespace MusicStreamer.CustomCommands
{
    

    // maybe this class should be spiltted into serveral "server-commands"
    class NavigateCommand : IBaseCommand
    {
        MainWindowViewModel _mwvm;

        public NavigateCommand(MainWindowViewModel mwvm)
        {
            this._mwvm = mwvm;
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

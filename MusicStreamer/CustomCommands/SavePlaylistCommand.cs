﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;

namespace MusicStreamer.CustomCommands
{
    class SavePlaylistCommand : ICommand
    {
        private MainWindowViewModel _mwvm;

        public SavePlaylistCommand(MainWindowViewModel mwvm)
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
    }
}

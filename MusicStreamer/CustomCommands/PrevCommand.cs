﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;
using MusicStreamer.ViewModels.Player;

namespace MusicStreamer.CustomCommands
{
    class PrevCommand : ICommand
    {
        CurrentSongViewModel _vm;

        public PrevCommand(CurrentSongViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _vm.PreviousSongInPlaylist();
        }
    }
}

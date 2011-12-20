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
    class SkipCommand : IBaseCommand
    {
        CurrentSongViewModel _vm;

        public SkipCommand(CurrentSongViewModel vm)
        {
            _vm = vm;
        }

        public void Execute(object parameter)
        {
            new Action(() =>
                {
                    try
                    {
                        _vm.NextSongInPlaylist();
                    }
                    catch (ArgumentOutOfRangeException e) { return; }
                }).BeginInvoke(null, null);
        }

        public void Execute()
        {
            Execute(null);
        }

        public void UnExecute()
        {
            throw new NotImplementedException();
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }
}

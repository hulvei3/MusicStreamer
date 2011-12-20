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
    class PrevCommand : IBaseCommand
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
            new Action(() =>
                {
                    try 
	                {	        
		                _vm.PreviousSongInPlaylist();
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
    }
}

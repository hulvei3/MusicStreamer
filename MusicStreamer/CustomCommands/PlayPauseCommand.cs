using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;

namespace MusicStreamer.CustomCommands
{
    class PlayPauseCommand : ICommand
    {
        private readonly CurrentSongViewModel _vm;

        public PlayPauseCommand(CurrentSongViewModel vm)
        {
            _vm = vm;
        }

        public void Execute(object parameter)
        {
            // parameter comes from the view (textBoxCurrentSong.Content)
            if (_vm.IsPlaying)
                // pause current song
                _vm.PauseCurrentSong();
            else if (_vm.HasPlayed)
                // resume current song
                _vm.PlayCurrentSong();
            else
                // play this new song
                _vm.PlayCurrentSong((string)parameter);
            
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}

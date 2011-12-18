using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;
using System.Windows.Forms;
using MusicStreamer.Interfaces;

namespace MusicStreamer.CustomCommands
{
    class ClearPlaylistCommand : IBaseCommand
    {
        MainWindowViewModel _mwvm;
        public ClearPlaylistCommand(MainWindowViewModel mwvm)
        {
            this._mwvm = mwvm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;



        public void Execute(object parameter)
        {

            DialogResult dr = MessageBox.Show("Do you want to save your current playlist before clearing the list?", "Clear playlist", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SaveFileDialog saveAs = new SaveFileDialog();
                saveAs.AddExtension = true;
                saveAs.DefaultExt = "nsp";
                saveAs.SupportMultiDottedExtensions = false;
                saveAs.Filter = "NAS Streamer Playlist (*.nsp)|*.nsp";
                if (saveAs.ShowDialog() == DialogResult.OK)
                {
                    _mwvm.Playlist.SavePlaylist(saveAs.FileName.ToString());
                }
                _mwvm.Playlist.ClearPlaylist();
            }
            else if (dr == DialogResult.No) { _mwvm.Playlist.ClearPlaylist(); }
            else
                return;
        }

        public void Execute()
        {

        }

        public void UnExecute()
        {
        }
    }
}

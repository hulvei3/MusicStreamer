using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MusicStreamer.Models;
using MusicStreamer.Exceptions;
using System.Windows;

namespace MusicStreamer.ViewModels
{
    class PlaylistViewModel : PropertyAndErrorHandler
    {
        public PlaylistViewModel(PlayerEngineModel player)
        {
            //MessageBox.Show(player.MediaPlayer.currentPlaylist.count.ToString());

            CurrentPlaylist = player.MediaPlayer.currentPlaylist;
            PlayListLibrary = player.MediaPlayer.playlistCollection;

            //StringBuilder sb = new StringBuilder();

            //sb.AppendLine(PlayListLibrary.);

            MessageBox.Show(sb.ToString());

            
        }

        public WMPLib.IWMPPlaylist CurrentPlaylist { get; set; }

        public WMPLib.IWMPPlaylistCollection PlayListLibrary { get; set; }

        

    }
}

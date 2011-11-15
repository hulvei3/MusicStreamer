using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MusicStreamer.Models;
using MusicStreamer.Exceptions;
using System.Windows;

using WMPLib;
using System.Collections.ObjectModel;

namespace MusicStreamer.ViewModels
{
    class PlaylistViewModel : PropertyAndErrorHandler
    {
        PlayerEngineModel _p;

        public PlaylistViewModel(PlayerEngineModel player)
        {
            _p = player;

            //MessageBox.Show(player.MediaPlayer.currentPlaylist.count.ToString());

            CurrentPlaylist = new ObservableCollection<Playlist.PlaylistItemViewModel>();

            PlayListLibrary = player.MediaPlayer.playlistCollection;


           

            // måske skal denne bruges senere??
            //player.MediaPlayer.NewStream += new WMPLib._WMPOCXEvents_NewStreamEventHandler(MediaPlayer_NewStream);


            

            // SETTING UP HANDLERS

            player.MediaPlayer.CurrentPlaylistChange += new WMPLib._WMPOCXEvents_CurrentPlaylistChangeEventHandler(MediaPlayer_CurrentPlaylistChange);

            
        }

        //void MediaPlayer_NewStream()
        //{
        //    throw new NotImplementedException();
        //}

        


        //public WMPLib.IWMPPlaylist CurrentPlaylist
        //{
        //    get { return (WMPLib.IWMPPlaylist)GetValue(CurrentPlaylistProperty); }
        //    set { SetValue(CurrentPlaylistProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for CurrentPlaylist.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CurrentPlaylistProperty =
        //    DependencyProperty.Register("CurrentPlaylist", typeof(WMPLib.IWMPPlaylist), typeof(PlaylistViewModel), new UIPropertyMetadata(
        //        OnPlaylistChanged));


        private ObservableCollection<Playlist.PlaylistItemViewModel> _currentPlaylist;

        public ObservableCollection<Playlist.PlaylistItemViewModel> CurrentPlaylist
        {
            get { return _currentPlaylist; }
            set { _currentPlaylist = value; }
        }

        public WMPLib.IWMPPlaylistCollection PlayListLibrary { get; set; }






        // impl. handlers

        void MediaPlayer_CurrentPlaylistChange(WMPLib.WMPPlaylistChangeEventType change)
        {

            String e = "CurrentPlaylist Changed event: " + change;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(e);
            sb.AppendLine("Items: "+CurrentPlaylist.Count);
            sb.AppendLine();

            MessageBox.Show(sb.ToString());

            OnPropertyChanged("CurrentPlaylist");
        }

        private static void OnPlaylistChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
          MessageBox.Show("dependency propery changed! " + "EventArgs: " + e);
        }

        internal void addNewMedia()
        {
            //IWMPMedia song = _p.MediaPlayer.newMedia(@"C:\Users\Morten Hulvej\Desktop\07 I Remember.mp3");

            Playlist.PlaylistItemViewModel song = new Playlist.PlaylistItemViewModel(@"C:\Users\Morten Hulvej\Desktop\07 I Remember.mp3");

            CurrentPlaylist.Add(song);
            OnPropertyChanged("CurrentPlaylist");
        }
    }
}

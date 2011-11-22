using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MusicStreamer.Models;
using MusicStreamer.Exceptions;
using System.Windows;

using WMPLib;
using System.Collections.ObjectModel;
using MusicStreamer.ViewModels.Playlist;

namespace MusicStreamer.ViewModels
{
    class PlaylistViewModel : PropertyAndErrorHandler
    {
        PlayerEngineModel _p;

        public PlaylistViewModel(PlayerEngineModel player)
        {
            _p = player;


            CurrentPlaylist = new ObservableCollection<PlaylistItemViewModel>();

            // hvis playlisten findes
            if (player.MediaPlayer.playlistCollection.getByName("streamerPlaylist_test").count > 0)
            {
                IWMPPlaylist firstlist = player.MediaPlayer.playlistCollection.getByName("streamerPlaylist_test").Item(0);

                for (int i = 0; i < firstlist.count; i++)
                {
                    var song = new PlaylistItemViewModel();
                    song.Url = firstlist.get_Item(i).sourceURL;
                    song.DurationAsString = firstlist.get_Item(i).durationString;
                    
                    CurrentPlaylist.Add(song);
                }
                
            }
            else // hvis ikke
            {
                
            }


            


           

            // måske skal denne bruges senere??
            //player.MediaPlayer.NewStream += new WMPLib._WMPOCXEvents_NewStreamEventHandler(MediaPlayer_NewStream);

            // SETTING UP HANDLERS

            player.MediaPlayer.CurrentPlaylistChange += new WMPLib._WMPOCXEvents_CurrentPlaylistChangeEventHandler(MediaPlayer_CurrentPlaylistChange);

            
        }
        
        private ObservableCollection<PlaylistItemViewModel> _currentPlaylist;
        public ObservableCollection<PlaylistItemViewModel> CurrentPlaylist
        {
            get { return _currentPlaylist; }
            set { _currentPlaylist = value; }
        }

        private PlaylistItemViewModel _selectedPlaylistItem;
        public PlaylistItemViewModel SelectedPlaylistItem
        {
            get { return _selectedPlaylistItem; }
            set
            {
                _selectedPlaylistItem = value;
                _p.MediaPlayer.URL = value.Url;
                _p.MediaPlayer.controls.play();
                OnPropertyChanged("SelectedPlaylistItem");
            }
        }


        public WMPLib.IWMPPlaylistCollection PlayListLibrary { get; set; }






        // impl. handlers

        void MediaPlayer_CurrentPlaylistChange(WMPLib.WMPPlaylistChangeEventType change)
        {

            //String e = "CurrentPlaylist Changed event: " + change;

            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine(e);
            //sb.AppendLine("Items: "+CurrentPlaylist.Count);
            //sb.AppendLine("Playlist changed outside MusicStreamer?");
            //MessageBox.Show(sb.ToString());

            OnPropertyChanged("CurrentPlaylist");
        }

        private static void OnPlaylistChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
          MessageBox.Show("dependency propery changed! " + "EventArgs: " + e);
        }

        internal void addNewMedia()
        {
            //IWMPMedia song = _p.MediaPlayer.newMedia(@"C:\Users\Morten Hulvej\Desktop\07 I Remember.mp3");

            //Playlist.PlaylistItemViewModel song = new Playlist.PlaylistItemViewModel(@"C:\Users\Morten Hulvej\Desktop\07 I Remember.mp3");

            //CurrentPlaylist.Add(song);
            OnPropertyChanged("CurrentPlaylist");
        }
    }
}

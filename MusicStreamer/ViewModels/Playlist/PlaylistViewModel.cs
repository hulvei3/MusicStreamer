using System;
using System.Text;

//using MusicStreamer.Models;
using MusicStreamer.Exceptions;
using System.Windows;
using System.Xml.Serialization;
using System.IO;

using WMPLib;
using System.Collections.ObjectModel;
using StreamerLib;

namespace MusicStreamer.ViewModels.Playlist
{
    
    class PlaylistViewModel : PropertyAndErrorHandler
    {

        string default_playlist = "streamerPlaylist_test";
        PlayerEngineModel _player;

        public PlaylistViewModel(PlayerEngineModel player)
        {
            _player = player;

            CurrentUIPlaylist = new ObservableCollection<PlaylistItemViewModel>();

            

            // måske skal denne bruges senere??
            //player.MediaPlayer.NewStream += new WMPLib._WMPOCXEvents_NewStreamEventHandler(MediaPlayer_NewStream);

            // SETTING UP HANDLERS

            _player.MediaPlayer.CurrentPlaylistChange += new WMPLib._WMPOCXEvents_CurrentPlaylistChangeEventHandler(MediaPlayer_CurrentPlaylistChange);

            
        }

        private PlaylistItemViewModel _playing;
        public PlaylistItemViewModel Playing
        { 
            get { return _playing; }
            set
            {
                if (value == null)
                    _playing = new PlaylistItemViewModel();
                else _playing = value;
                OnPropertyChanged("Playing");
            }
        }

        private ObservableCollection<PlaylistItemViewModel> _currentPlaylist;
        public ObservableCollection<PlaylistItemViewModel> CurrentUIPlaylist
        {
            get { return _currentPlaylist; }
            set 
            { 
                _currentPlaylist = value;
                OnPropertyChanged("CurrentUIPlaylist");
            }
            
        }

        private PlaylistItemViewModel _selectedPlaylistItem;
        public PlaylistItemViewModel SelectedPlaylistItem
        {
            get { return _selectedPlaylistItem; }
            set
            {
                _selectedPlaylistItem = value;

                // plays selected song immediately  !SHOULD NOT BE PLACED IN HERE!
                //_p.MediaPlayer.URL = value.Url;
                //_p.MediaPlayer.controls.play();

                OnPropertyChanged("SelectedPlaylistItem");
            }
        }

        public IWMPPlaylist CurrentDLLPlaylist { get; set; }
        public WMPLib.IWMPPlaylistCollection PlayListLibrary { get; set; }

        #region test-code
        public void OpenTestPlaylist()
        {
            // hvis playlisten findes
            if (_player.MediaPlayer.playlistCollection.getByName(default_playlist).count > 0)
            {
                IWMPPlaylist firstlist = _player.MediaPlayer.playlistCollection.getByName(default_playlist).Item(0);

                // wmp.dll <-- current playlist
                _player.MediaPlayer.currentPlaylist = firstlist;

                // update UI playlist
                for (int i = 0; i < firstlist.count; i++)
                {
                    var song = new PlaylistItemViewModel(firstlist.get_Item(i).sourceURL);
                    song.DurationAsString = firstlist.get_Item(i).durationString;

                    CurrentUIPlaylist.Add(song);
                    
                }

            }
            else // hvis ikke
            {
                MessageBox.Show(string.Format("WMP.dll indeholder ikke playlisten: {0}", default_playlist));
            }
        }
        #endregion


        // impl. handlers

        void MediaPlayer_CurrentPlaylistChange(WMPPlaylistChangeEventType change)
        {

            String e = "CurrentPlaylist Changed event: " + change;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(e);
            sb.AppendLine("Items: " + _player.MediaPlayer.currentPlaylist.count);
            sb.AppendLine("Playlist changed outside MusicStreamer?");
            MessageBox.Show(sb.ToString());

            OnPropertyChanged("CurrentUIPlaylist");
        }

        // adds song to playlist (not loaded yet)
        internal void AddToPlaylist(PlaylistItemViewModel song)
        {
                IWMPMedia media = _player.MediaPlayer.newMedia(song.Url);

                if (media.durationString != null || media.durationString != "0")
                    song.DurationAsString = media.durationString;
                else
                    song.DurationAsString = "--";

                // TEST
                //ShowFileInfo(media);

                CurrentUIPlaylist.Add(song);
                
            

        }

        internal void RemoveFromPLaylist(PlaylistItemViewModel song)
        {
            CurrentUIPlaylist.Remove(song);
        }

        public PlaylistItemViewModel GetNextSong()
        {
            PlaylistItemViewModel next = null;

            var index = CurrentUIPlaylist.IndexOf(Playing);

            if (index < CurrentUIPlaylist.Count - 1)
                next = CurrentUIPlaylist[index + 1];
            else if (index == CurrentUIPlaylist.Count - 1)
            {
                // TODO  repeat!
                next = CurrentUIPlaylist[0];
            }

            if (next != null)
                SelectedPlaylistItem = next;

            return next;
        }

        public PlaylistItemViewModel GetPreviousSong()
        {
            PlaylistItemViewModel prev = null;

            var index = CurrentUIPlaylist.IndexOf(Playing);

            
            if (index > 0)
            {
                prev = CurrentUIPlaylist[index - 1];
            }

            if (prev != null)
                SelectedPlaylistItem = prev;

            return prev;
        }

        public void ShowFileInfo(IWMPMedia media)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("Media-info for " + media.name);
            sb.AppendLine();
            sb.AppendLine("Attributes:");
            for (int att = 0; att < media.attributeCount; att++)
            {
                string attName = media.getAttributeName(att);
                sb.AppendFormat("{0}: {1}", att, attName);
                sb.AppendLine();
                sb.AppendFormat(" -> {0}", media.getItemInfo(attName));
                sb.AppendLine();
                
            }
            sb.AppendLine();
            sb.AppendLine("Other:");
            sb.AppendLine();
            sb.AppendFormat("Marker-count: {0}",media.markerCount);
            
            MessageBox.Show(sb.ToString());
        }

        public void SavePlaylist(String destination)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(ObservableCollection<PlaylistItemViewModel>));
            
            TextWriter textStream = new StreamWriter(destination);
            
            mySerializer.Serialize(textStream,CurrentUIPlaylist);
            textStream.Flush();
            textStream.Close();
        }

        public void LoadPlaylist(String selectedFile)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(ObservableCollection<PlaylistItemViewModel>));
            FileStream fs = new FileStream(selectedFile, FileMode.Open);

            CurrentUIPlaylist = (ObservableCollection<PlaylistItemViewModel>) mySerializer.Deserialize(fs);
        }

        internal void ClearPlaylist()
        {
            CurrentUIPlaylist.Clear();
        }
    }
}

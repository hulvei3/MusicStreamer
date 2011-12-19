using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MusicStreamer.Exceptions;
using System.ComponentModel;
using System.Windows.Input;

namespace MusicStreamer.ViewModels.Playlist
{
    [Serializable]
    public class PlaylistItemModel : INotifyPropertyChanged
    {

        void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public PlaylistItemModel() { }

        public PlaylistItemModel(string url)
        {
            if (url.Equals("[dummyItem]"))
                Name = "";
            else
            {
                Url = url;

                int temp = Url.LastIndexOf("/") + 1;
                Name = Url.Substring(temp);
            }
        }

        private string _url = "";
        public string Url
        {
            get { return _url; }
            set { _url = value; OnPropertyChanged("Url"); }
        }

        private int _length;
        public int Length 
        {
            get { return _length; }
            set { _length = value; OnPropertyChanged("Length"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public string DurationAsString { get; set; }

        public RoutedCommand RemoveCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MusicStreamer.Exceptions;
using System.ComponentModel;

namespace MusicStreamer.ViewModels.Playlist
{
    [Serializable]
    public class PlaylistItemViewModel : INotifyPropertyChanged
    {

        void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        /*

        #region Name dependency property

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(PlaylistItemViewModel), new UIPropertyMetadata("N/A"));
        #endregion
        */

        public PlaylistItemViewModel()
        {
        }

        public PlaylistItemViewModel(string url)
        {
            Url = url;
            Name = _name;
            
        }

        public string _url = "";
        public string Url
        {
            get { return _url; }
            set { _url = value; OnPropertyChanged("Url"); }
        }

        public int _length;
        public int Length 
        {
            get { return _length; }
            set { _length = value; OnPropertyChanged("Length"); }
        }

        public string _name = "";
        public string Name
        {
            get { return _name; }
            set 
            {          
                int temp = Url.LastIndexOf("/")+1;
                _name = Url.Substring(temp);
                
                OnPropertyChanged("Name"); 
            }
        }

        public string DurationAsString { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

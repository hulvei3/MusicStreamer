using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MusicStreamer.ViewModels.Server
{
    class ServerlistItemViewModel : INotifyPropertyChanged
    {
        private string _url;
        private string _size;

        public ServerlistItemViewModel(string url)
        {
            Url = url;
        }

        public ServerlistItemViewModel(string url, string size)
        {

            Url = url;
            Size = size;
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

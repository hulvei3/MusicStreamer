using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace MusicStreamer.ViewModels.Server
{
    class ServerlistItemViewModel : DependencyObject, INotifyPropertyChanged
    {
        private string _url;
        private double _size;

        public ServerlistItemViewModel(string url, double size)
        {
            this._url = url;
            this._size = size;
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public double Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

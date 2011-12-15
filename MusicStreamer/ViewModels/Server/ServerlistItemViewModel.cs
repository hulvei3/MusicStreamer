using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicStreamer.ViewModels.Server
{
    class ServerlistItemViewModel : INotifyPropertyChanged
    {
        private string _url;
        private string _size;

        public ServerlistItemViewModel(string name, string size)
        {
            Url = name;
<<<<<<< HEAD
=======
            Name = name;
>>>>>>> 7d36a33e31396d69709d175f831a2b8b96fdc731
            Size = size;
            Name = name;
        }
        public string Name { get; set; }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        public string Name { get; set; }

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }
        public RoutedCommand AddCommand{ get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

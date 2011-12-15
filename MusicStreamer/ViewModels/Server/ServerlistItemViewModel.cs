<<<<<<< HEAD
﻿using System;
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
            Name = name;
            Size = size;
        }

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
=======
﻿using System;
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
            Size = size;
            Name = name;
        }
        public string Name { get; set; }

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
        public RoutedCommand AddCommand{ get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
>>>>>>> 47fd055002a810fb8f1b8379868f111a6d56ba35

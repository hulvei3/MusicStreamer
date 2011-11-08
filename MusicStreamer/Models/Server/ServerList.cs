using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace MusicStreamer.Models.Server
{
    public class ServerList : System.Collections.ObjectModel.ObservableCollection<Song>
    {
        public ServerList() : base()
        {
            //Add(new Song("Willa", 23));
            //Add(new Song("Isak", 40));
            //Add(new Song("Victor",2));
            //Add(new Song("Jules", 30));
        }
    }

    public class Song
    {
        private string _url;
        private double _size;

        public Song(string url, double size)
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
    }

}

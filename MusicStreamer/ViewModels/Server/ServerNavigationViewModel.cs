using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using MusicStreamer.Exceptions;
using MusicStreamer.Models.Server;
using System.Collections.Specialized;

namespace MusicStreamer.ViewModels.Server
{
    class ServerNavigationViewModel : PropertyAndErrorHandler
    {
        private String _currentLocation;
        private ServerConnectionViewModel _scvm;
        private ServerList _currentList;

        public ServerNavigationViewModel(ServerConnectionModel scm)
        {
            _scvm = new ServerConnectionViewModel(scm);
            CurrentLocation = scm.Host;
        }

        //Property for current list of files to show
        public ServerList CurrentList
        {
            get { return _currentList; }
            set
            {
                _currentList = value;
                OnPropertyChanged("CurrentList");
            }
        }


        //URL
        public String CurrentLocation
        {
            get { return _currentLocation; }
            set{_currentLocation = value + "/";}
        }

        public ServerList Navigate(string url)
        {
            // set new cuurent dir to new url
            _scvm.NewURL(CurrentLocation + url);
            CurrentLocation = CurrentLocation + url;
            return Navigate();
        }
        public ServerList Navigate()
        {
            // set new cuurent dir to new url
            _scvm.NewURL(CurrentLocation);
            FtpWebResponse resp = _scvm.ListCurrentDir();
            CurrentList = listFiles(resp, false);
            //CurrentList = new ServerList();
            return CurrentList;
        }

        //Sorts the files and only shows the files you want.
        private ServerList listFiles(FtpWebResponse files, Boolean showAllFiles)
        {
            Stream responseStream = files.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            String filesFolders = reader.ReadToEnd();

            

            IList<String> fileArray = filesFolders.Split('\n').ToList<String>();
            ServerList serverList = new ServerList();

            if (!showAllFiles)
            {
                foreach (String s in fileArray)
                {
                    if (s.Contains(".mp3") || s.StartsWith("d")) //Feel free to add the filetypes you like.
                    {
                        if (s.Length > 0)
                        {
                            Char[] c = s.ToCharArray();
                            for (int i = 0; i < c.Length; i++)
                            {
                                if (c[i] == ':')
                                {
                                    serverList.Add(new Song(s.Substring(i + 4), 0.0));
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (String s in fileArray)
                {
                    serverList.Add(new Song(s,0.0)); //Adds all directorydetails
                }
            }
            //newList.Sort();
            return serverList;
        }

        public void levelUp(String url)
        {
            
        }
    }
}

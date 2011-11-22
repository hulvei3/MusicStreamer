using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using MusicStreamer.Exceptions;
using MusicStreamer.Models.Server;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Windows;

namespace MusicStreamer.ViewModels.Server
{
    class ServerNavigationViewModel : PropertyAndErrorHandler
    {
        private String _currentLocation;
        private ServerConnectionViewModel _scvm;
        private ObservableCollection<ServerlistItemViewModel> _currentList;

        public ServerNavigationViewModel()
        {
            CurrentList = new ObservableCollection<ServerlistItemViewModel>();
        }

        // skal sandsynligvis ikke bruges
        public ServerNavigationViewModel(ServerConnectionModel scm)
        {
            _scvm = new ServerConnectionViewModel(scm);
            CurrentLocation = scm.Host;
        }

        public void setConnectionModel(ServerConnectionModel scm)
        {
            _scvm = new ServerConnectionViewModel(scm);
            CurrentLocation = scm.Host;
        }

        //Property for current list of files to show
        public ObservableCollection<ServerlistItemViewModel> CurrentList
        {
            get { return _currentList; }
            set
            {
                _currentList = value;
                OnPropertyChanged("CurrentList");
                //StringBuilder list = new StringBuilder();
                //foreach (ServerlistItemViewModel s in _currentList)
                //{
                //    list.AppendLine(s.Url);
                //}
                //MessageBox.Show(list.ToString());
            }
        }


        //URL
        public String CurrentLocation
        {
            get { return _currentLocation; }
            set{_currentLocation = value + "/";}
        }

        public ObservableCollection<ServerlistItemViewModel> Navigate(string url)
        {
            // set new cuurent dir to new url
            _scvm.NewURL(CurrentLocation + url);
            CurrentLocation = CurrentLocation + url;
            return Navigate();
        }
        public ObservableCollection<ServerlistItemViewModel> Navigate()
        {
            // set new cuurent dir to new url
            _scvm.NewURL(CurrentLocation);
            FtpWebResponse resp = _scvm.ListCurrentDir();
            CurrentList = listFiles(resp, false);
            //CurrentList = new ServerList();
            return CurrentList;
        }

        //Sorts the files and only shows the files you want.
        private ObservableCollection<ServerlistItemViewModel> listFiles(FtpWebResponse files, Boolean showAllFiles)
        {
            Stream responseStream = files.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            String filesFolders = reader.ReadToEnd();

            

            IList<String> fileArray = filesFolders.Split('\n').ToList<String>();
            ObservableCollection<ServerlistItemViewModel> serverList = new ObservableCollection<ServerlistItemViewModel>();

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
                                    serverList.Add(new ServerlistItemViewModel(s.Substring(i + 4), 0.0));
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
                    serverList.Add(new ServerlistItemViewModel(s,0.0)); //Adds all directorydetails
                }
            }
            //newList.Sort();
            return serverList;
        }

        public void levelUp(String url)
        {
            
        }

        public void add_to_list_test()
        {
            CurrentList.Add(new ViewModels.Server.ServerlistItemViewModel("test_url", 3));
            OnPropertyChanged("CurrentList");
        }
    }
}

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
        public int selectItemCount = 0;
        private String _currentLocation;
        private ServerConnectionViewModel _scvm;
        private ObservableCollection<ServerlistItemViewModel> _currentList;
        private ServerlistItemViewModel _selectedItem;

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
                _currentList.Insert(0, new ServerlistItemViewModel("[Parent directory..]"));
                OnPropertyChanged("CurrentList");
                //StringBuilder list = new StringBuilder();
                //foreach (ServerlistItemViewModel s in _currentList)
                //{
                //    list.AppendLine(s.Url);
                //}
                //MessageBox.Show(list.ToString());
            }
        }

        public ServerlistItemViewModel SelectedServerListItem
        {
            get{return _selectedItem;}
            set
            {
                _selectedItem = value;
                 
                if (value.Url.Equals("[Parent directory..]"))
                {
                    LevelUp();
                    Navigate();
                }
                else if (value.Url.EndsWith(".mp3"))
                {
                    AddToPlayList(value.Url);
                    Navigate();
                }
                else
                    Navigate(value.Url);
            }
            
        }

        //URL
        public String CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
               
                _currentLocation = value + "/";

                OnPropertyChanged("CurrentLocation");
            }
            
        }

        public void AddToPlayList(String file)
        {
            MessageBox.Show("Press \"+\" to add song to playlist");
            
        }

        public void LevelUp()
        {
            //MessageBox.Show(CurrentLocation);
            int index = CurrentLocation.LastIndexOf('/');
            String parentDirectory = CurrentLocation.Remove(index);
            int index2 = parentDirectory.LastIndexOf('/');
            parentDirectory = CurrentLocation.Remove(index2);
            CurrentLocation = parentDirectory;
            //MessageBox.Show(parentDirectory); 
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
            FtpWebResponse resp = null;
            //try
            //{
                resp = _scvm.ListCurrentDir();
            //}
            //catch (MusicStreamerException e)
            //{
                
            //    //Handle exception here
            //}
          
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
            //MessageBox.Show(filesFolders);
                    
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
                                    
                                    string st = s.Substring(i + 4);
                                    string size = "";
                           
                                    if (s.StartsWith("d")) { size = ""; }
                                    else
                                    {
                                        size = "To Be Updated";
                                    }
                                    serverList.Add(new ServerlistItemViewModel( st.Substring(0,st.Length-1), size));
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
                    serverList.Add(new ServerlistItemViewModel(s,"0")); //Adds all directorydetails
                }
            }
            //newList.Sort();
            return serverList;
        }

        private string SizeConverter()
        {
            
            return "mb";
        }



        //public void add_to_list_test()
        //{
        //    CurrentList.Add(new ViewModels.Server.ServerlistItemViewModel("test_url", 3));
        //    OnPropertyChanged("CurrentList");
        //}
    }
}

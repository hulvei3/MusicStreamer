using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using MusicStreamer.Exceptions;
//using MusicStreamer.Models.Server;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Windows;
using StreamerLib;

namespace MusicStreamer.ViewModels.Server
{
    class ServerNavigationViewModel : PropertyAndErrorHandler
    {
        private String _currentLocation;
        private String _oldCurrentLocation;
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
                int slashCount = 0;
                _currentList = value;
                if (CurrentLocation != null)
                {
                    char[] charArray = CurrentLocation.ToCharArray();
                    for (int i = 0; i < charArray.Length;i++ )
                    {
                        if (charArray[i].Equals('/'))
                        {
                            slashCount++;
                        }
                    }
                    if (slashCount > 3)
                    {
                        _currentList.Insert(0, new ServerlistItemViewModel("[Parent directory..]", ""));

                    }
                }
                OnPropertyChanged("CurrentList");
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
                else if (value.Url.EndsWith(Res.Filetypes.MP3) || value.Url.EndsWith(Res.Filetypes.WAV) || value.Url.EndsWith(Res.Filetypes.WMA))
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
                OldCurrentLocation = _currentLocation;
                _currentLocation = value.TrimEnd('/')+"/";
                
                OnPropertyChanged("CurrentLocation");
            }
            
        }

        public String OldCurrentLocation
        {
            get { return _oldCurrentLocation; }
            set
            {
                _oldCurrentLocation = value;
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
            try
            {
                _scvm.NewURL(CurrentLocation + url);
                CurrentLocation = CurrentLocation + url;
                OldCurrentLocation = CurrentLocation;
            }
            catch (UriFormatException e)
            {
                _scvm.NewURL(OldCurrentLocation);
                CurrentLocation = OldCurrentLocation;
            }

            return Navigate();
        }
        public ObservableCollection<ServerlistItemViewModel> Navigate()
        {
            // set new cuurent dir to new url
            
            try
            {
                _scvm.NewURL(CurrentLocation);
                FtpWebResponse resp = null;
                //resp = _scvm.ListCurrentDirDetails();
                //CurrentList = listFilesDetails(resp, false);
                resp = _scvm.ListCurrentDir();
                CurrentList = listFiles(resp);

            }
            catch (MusicStreamerException e)
            {
                _scvm.NewURL(OldCurrentLocation);
                CurrentLocation = OldCurrentLocation;

            }
          
            
            //CurrentList = new ServerList();
            return CurrentList;
        }

        private IList<String> readFolderToString(FtpWebResponse files)
        {
            Stream responseStream = files.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            String filesFolders = reader.ReadToEnd();

            return filesFolders.Split('\n').ToList<String>();
        }
        private ObservableCollection<ServerlistItemViewModel> listFiles(FtpWebResponse files)
        {
            IList<String> fileArray = readFolderToString(files);

            ObservableCollection<ServerlistItemViewModel> serverList = new ObservableCollection<ServerlistItemViewModel>();

            foreach (String s in fileArray)
            {
                if (s.Length > 0)
                {
                    if (s.EndsWith("\r"))
                    {
                        String name = s.Substring(0, s.Length - 1);
                        var listItem = new ServerlistItemViewModel(name, "");
                        listItem.AddCommand = MainWindowViewModel.Instance.CommandLib.AddToPlaylistCommand;
                        serverList.Add(listItem);
                    }
                    else
                    {
                        var listItem = new ServerlistItemViewModel(s, "");
                        listItem.AddCommand = MainWindowViewModel.Instance.CommandLib.AddToPlaylistCommand;
                        serverList.Add(listItem);
                    }
                }
            }    
            return serverList;
        }

        //Sorts the files and only shows the files you want.
        private ObservableCollection<ServerlistItemViewModel> listFilesDetails(FtpWebResponse files, Boolean showAllFiles)
        {

            IList<String> fileArray = readFolderToString(files);
            
            ObservableCollection<ServerlistItemViewModel> serverList = new ObservableCollection<ServerlistItemViewModel>();

            if (!showAllFiles)
            {
                foreach (String s in fileArray)
                {
                    if (s.Contains(Res.Filetypes.MP3) || s.Contains(Res.Filetypes.WMA) || s.Contains(Res.Filetypes.WAV) || s.StartsWith("d")) //Feel free to add the filetypes you like.
                    {
                        if (s.Length > 0)
                        {
                            Char[] c = s.ToCharArray();
                            for (int i = 0; i < c.Length; i++)
                            {
                                if (c[i] == ':')
                                {

                                    string name = s.Substring(i + 4);
                                    string size = "";

                                    if (s.StartsWith("d")) { size = ""; }
                                    else
                                    {
                                        size = "To Be Updated";
                                    }
                                    var listItem = new ServerlistItemViewModel(name = name.Substring(0, name.Length - 1), size);
                                    listItem.AddCommand = MainWindowViewModel.Instance.CommandLib.AddToPlaylistCommand;
                                    serverList.Add(listItem);
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
    }
}

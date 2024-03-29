﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using MusicStreamer.Exceptions;
using StreamerLib;
using System.Windows.Input;

namespace MusicStreamer.ViewModels.Server
{
    class ServerNavigationViewModel : PropertyAndErrorHandler
    {
        private String _currentLocation;
        private String _oldCurrentLocation;
        private ServerConnectionViewModel _scvm;
        private ObservableCollection<ServerlistItemModel> _currentList;
        private ServerlistItemModel _selectedItem;

        public ServerNavigationViewModel()
        {
            CurrentList = new ObservableCollection<ServerlistItemModel>();
            IsNavigating = Visibility.Hidden;
        }

        public void setConnectionModel(ServerConnectionModel scm)
        {
            _scvm = new ServerConnectionViewModel(scm);
            CurrentLocation = scm != null ? scm.Host : null;
        }

        public ServerConnectionModel CurrentServer { get { return _scvm.GetModel(); } }

        //Property for current list of files to show
        public ObservableCollection<ServerlistItemModel> CurrentList
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
                        _currentList.Insert(0, new ServerlistItemModel("[Parent directory..]",""));
                    }
                }
                OnPropertyChanged("CurrentList");
            }
        }

        public ServerlistItemModel SelectedServerListItem
        {
            get{return _selectedItem;}
            set
            {
                Mouse.OverrideCursor = Cursors.Wait;
                var _selectedItem = value;
                IsNavigating = Visibility.Visible;
                //new Action(() =>
                //{
                    if (value.Url.Equals("[Parent directory..]"))
                    {
                        LevelUp();
                        Navigate();
                    }
                    else if (_selectedItem.Url.EndsWith(Res.Filetypes.MP3) || _selectedItem.Url.EndsWith(Res.Filetypes.WAV) || _selectedItem.Url.EndsWith(Res.Filetypes.WMA))
                    {
                        AddToPlayList(_selectedItem.Url);
                        Navigate();
                    }
                    else
                        Navigate(_selectedItem.Url);
                    IsNavigating = Visibility.Hidden;
                //}).BeginInvoke(null, null);
                    Mouse.OverrideCursor = null;
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

        public ObservableCollection<ServerlistItemModel> Navigate(string url)
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

        public ObservableCollection<ServerlistItemModel> Navigate()
        {
            IsNavigating = Visibility.Visible;
            // set new cuurent dir to new url
            try
            {
                _scvm.NewURL(CurrentLocation);
                //FtpWebResponse resp = null;
                //Not used anymore
                //resp = _scvm.ListCurrentDirDetails();
                //CurrentList = listFilesDetails(resp, false);
                Response = _scvm.ListCurrentDir();
                CurrentList = listFiles();
            }
            catch (MusicStreamerException e)
            {
                try
                {
                    _scvm.NewURL(OldCurrentLocation);
                    CurrentLocation = OldCurrentLocation;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            IsNavigating = Visibility.Hidden;
            return CurrentList;
        }
        private Visibility _isNavigating;
        public Visibility IsNavigating { get { return _isNavigating; } private set { _isNavigating = value; OnPropertyChanged("IsNavigating"); } }

        private FtpWebResponse Response
        {
            get;
            set;
        }

        private IList<String> readFolderToString()
        {
            Stream responseStream = Response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            String filesFolders = reader.ReadToEnd();

            return filesFolders.Split('\n').ToList<String>();
        }

        private ObservableCollection<ServerlistItemModel> listFiles()
        {
            IList<String> fileArray = readFolderToString();
            
            ObservableCollection<ServerlistItemModel> serverList = new ObservableCollection<ServerlistItemModel>();

            foreach (String s in fileArray)
            {
                if (s.Length > 0)
                {
                        if (s.EndsWith("\r"))
                        {
                            string tempName;
                            String name = s.Substring(0, s.Length - 1);
                            try{
                                tempName = name.Substring(name.Length-5);
                            }
                            catch(ArgumentOutOfRangeException e)
                            {
                                tempName = name;
                            }
                            if (!Path.HasExtension(tempName) || tempName.EndsWith(Res.Filetypes.MP3) || tempName.EndsWith(Res.Filetypes.WAV) || tempName.EndsWith(Res.Filetypes.WMA))
                            {
                                var listItem = new ServerlistItemModel(name, "");
                                listItem.AddCommand = MainWindowViewModel.Instance.CommandLib.AddToPlaylistCommand;
                                serverList.Add(listItem);
                            }
                        }
                        else
                        {
                            string tempName;
                            try
                            {
                                tempName = s.Substring(s.Length - 5);
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                tempName = s;
                            }
                            if (!Path.HasExtension(tempName) || s.EndsWith(Res.Filetypes.MP3) || s.EndsWith(Res.Filetypes.WAV) || s.EndsWith(Res.Filetypes.WMA))
                            {
                                var listItem = new ServerlistItemModel(s, "");
                                listItem.AddCommand = MainWindowViewModel.Instance.CommandLib.AddToPlaylistCommand;
                                serverList.Add(listItem);
                            }
                        }
                    
                }
            }
            
            return serverList;
        }

        #region Not Used anymore
        //Sorts the files and only shows the files you want.
        //private ObservableCollection<ServerlistItemViewModel> listFilesDetails(FtpWebResponse files, Boolean showAllFiles)
        //{

        //    IList<String> fileArray = readFolderToString(files);
            
        //    ObservableCollection<ServerlistItemViewModel> serverList = new ObservableCollection<ServerlistItemViewModel>();

        //    if (!showAllFiles)
        //    {
        //        foreach (String s in fileArray)
        //        {
        //            //if (s.Contains(Res.Filetypes.MP3) || s.Contains(Res.Filetypes.WMA) || s.Contains(Res.Filetypes.WAV) || s.StartsWith("d")) //Feel free to add the filetypes you like.
        //            //{
        //            //    if (s.Length > 0)
        //            //    {
        //            if (s.Contains(Res.Filetypes.MP3) || s.StartsWith("d")) //Feel free to add the filetypes you like.
        //            {
        //                if (s.Length > 0)
        //                {
        //                    //Char[] c = s.ToCharArray();
        //                    //for (int i = 0; i < c.Length; i++)
        //                    //{
        //                    //    if (c[i] == ':')
        //                    //    {

        //                    //        string name = s.Substring(i + 4);
        //                    //        string size = "";

        //                    //        if (s.StartsWith("d")) { size = ""; }
        //                    //        else
        //                    //        {
        //                    //            size = "To Be Updated";
        //                    //        }
        //                            var listItem = new ServerlistItemViewModel(s.Substring(0,s.Length-1), "");
        //                            listItem.AddCommand = MainWindowViewModel.Instance.CommandLib.AddToPlaylistCommand;
        //                            serverList.Add(listItem);
        //                        //}
        //                    }
        //                }
        //            }
                
        //    }
        //    else
        //    {
        //        foreach (String s in fileArray)
        //        {
        //            serverList.Add(new ServerlistItemViewModel(s,"0")); //Adds all directorydetails
        //        }
        //    }
        //    //newList.Sort();
        //    return serverList;
        //}
        #endregion
    }
}

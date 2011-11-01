using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using MusicStreamer.Exceptions;
using MusicStreamer.Models.Server;

namespace MusicStreamer.ViewModels.Server
{
    class ServerNavigationViewModel : PropertyAndErrorHandler
    {
        private String _currentLocation;
        private ServerConnectionViewModel _scvm;

        public ServerNavigationViewModel(ServerConnectionModel scm)
        {
            _scvm = new ServerConnectionViewModel(scm);
            CurrentLocation = scm.Host;
        }


        public String CurrentLocation
        {
            get { return _currentLocation; }
            set{_currentLocation = value + "/";}
        }

        public List<String> Navigate(string url)
        {
            // set new cuurent dir to new url
            _scvm.NewURL(CurrentLocation + url);
            CurrentLocation = CurrentLocation + url;
            FtpWebResponse resp = _scvm.ListCurrentDir();
            return listFiles(resp, false);
        }

        //Sorts the files and only shows the files you want.
        private List<String> listFiles(FtpWebResponse files, Boolean showAllFiles)
        {
            Stream responseStream = files.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            String filesFolders = reader.ReadToEnd();


            List<String> fileArray = filesFolders.Split('\n').ToList<String>();
            List<String> newList = new List<string>();

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
                                    newList.Add(s.Substring(i + 4));
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
                    newList.Add(s); //Adds all directorydetails
                }
            }
            newList.Sort();
            return newList;
        }

        public void levelUp(String url)
        {

        }

    }
}

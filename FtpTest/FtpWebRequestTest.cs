using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace FtpWebRequestConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.run();
        }

        public void run()
        {
            //Put your own userinfo here
            String ip = "xxx"; //Morten
            String user = "xxx";
            String pw = "xxx";
            FtpWebResponse musicFiles;
            List<String> musicFilesArray;
            Boolean showAllFiles = false;

            do{

                Console.WriteLine("Connecting to " + ip);

                musicFiles = connect(ip, user, pw);

                if (musicFiles != null)
                {
                    musicFilesArray = listFiles(musicFiles, showAllFiles);

                    foreach (String i in musicFilesArray)
                    {
                        Console.WriteLine(i);
                    }
                }

                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Not able to connect to the ftp server...");
                }

                Console.WriteLine();
                Console.WriteLine("Write the directory you want to enter:");
                
                ip += "/"+ Console.ReadLine();
            } while (ip!=null);
        }


        //Connects to at specific ftp and returns a String that contains the folders and files
        public FtpWebResponse connect(String ip, String user, String pw)
        {
            FtpWebResponse result = null;

            FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://" + ip);
            req.Credentials = new NetworkCredential(user, pw);
            req.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            req.Proxy = null;

            try
            {
                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                result = response;
            
            }
            catch (WebException e)
            {
                //Console.WriteLine(e.GetBaseException());
                result = null;
            }

            return result;
        }

        //Sorts the files and only shows the files you want.
        public List<String> listFiles(FtpWebResponse files, Boolean showAllFiles)
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
    }
}
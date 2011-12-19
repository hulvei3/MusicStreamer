using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicStreamer;
using System.Net;
using System.IO;


namespace FtpTest
{
    class GetFileSizeTest
    {
        private FtpWebRequest Request { get; set; }

        private long remFileSize { get; set; }

        static void Main(string[] args)
        {
            GetFileSizeTest prog = new GetFileSizeTest();
            prog.run();

        }

        public void run()
        {
            //Put your own userinfo here
            String user = "xxx";
            String pw = "xxx";
            String host = "xxx";

            //Console.WriteLine(host, user, pw);

            //Request = (FtpWebRequest)WebRequest.Create(host);

            //Request.Credentials = new NetworkCredential(user, pw);
            //Request.UseBinary = true;
            //Request.Method = null;
            ////Request.Method = WebRequestMethods.Ftp.GetFileSize;
            //Request.Proxy = null;


            //FtpWebResponse response = (FtpWebResponse)Request.GetResponse();
            //response = (FtpWebResponse)Request.GetResponse();
            //remFileSize = response.ContentLength;
            //Stream responseStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(responseStream);
            //String s = reader.ReadToEnd();
            //long size = remFileSize;
            //Console.WriteLine(size);
            //Console.WriteLine(s);

            //string q = Console.ReadLine();

            //MusicStreamer.ViewModels.Server.ServerNavigationViewModel snvm = new MusicStreamer.ViewModels.Server.ServerNavigationViewModel();

            WebClient wc = new WebClient();
            wc.Credentials = new NetworkCredential(user, pw);

            wc.OpenRead("ftp://90.184.75.15/OpenShare/Music/classical.WAV");


            string bytes_total = wc.ResponseHeaders["Content-Length"];
            Console.WriteLine(bytes_total + " Bytes");
            string q = Console.ReadLine();

        }

        
    }
}

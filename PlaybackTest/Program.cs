using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlaybackTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //test1();          
            test2();
        }

        public static void test2()
        {

            WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        
            WMPLib.IWMPMedia media = player.newMedia("ftp://90.184.75.15/OpenShare/Music/Morten/Deadmau5/Random Album Title/07 I Remember.mp3");

            player.NewStream += new WMPLib._WMPOCXEvents_NewStreamEventHandler(player_NewStream);

            player.controls.playItem(media);


        }

        static void player_NewStream()
        {
            throw new NotImplementedException();
        }

        public static void test1()
        {
            WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();


            player.settings.autoStart = false;
            player.URL = @"C:\Users\Morten Hulvej\Desktop\07 I Remember.mp3";


            player.controls.play();


            for (int i = 10; i > 0; i--)
            {
                Console.WriteLine("Going to sleep for " + i + " seconds..");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMPLib;

namespace MusicStreamer
{
    class PlayerEngine
    {
        private WindowsMediaPlayer mediaPlayer = null;
        private static PlayerEngine player = null;

        // singleton 
        public static PlayerEngine instance
        {
            get
            {
                if (player == null)
                    player = new PlayerEngine();
                return player;
            }
        }

        // gets remaining time of current song (in seconds)
        public double RemainingTime
        {
            get { if (mediaPlayer != null)
                       return Math.Floor(mediaPlayer.currentMedia.duration - mediaPlayer.controls.currentPosition);
                  return 0;
            }
        }

        // gets/sets current duration of current song (in seconds)
        public double CurrentTime
        {
            get
            {
                if (mediaPlayer != null)
                    return mediaPlayer.controls.currentPosition;
                return 0;
            }
            set
            {
                if (mediaPlayer != null)
                    mediaPlayer.controls.currentPosition = value;
            }
        }

        // checks to see if the player is initiated
        private Boolean PlayerInitiated
        {
            get { return mediaPlayer != null; }
        }

        private PlayerEngine()
        {
            mediaPlayer = new WindowsMediaPlayer();
            mediaPlayer.settings.autoStart = false;
        }
        
        // setting current song to the file from 'filepath'
        public void loadFile(String filepath)
        {
            if (mediaPlayer == null)
                return;

            mediaPlayer.URL = filepath;

            
        }

        // plays the file from the 'URL'-property
        public void playFile()
        {
            if (mediaPlayer == null)
                return;

           
            mediaPlayer.controls.play();
            
        }

        // pauses the current song
        public void pauseFile()
        {
            mediaPlayer.controls.pause();
        }

        // stops the current song
        public void stopFile()
        {
            mediaPlayer.controls.stop();
        }

        public void closePlayer()
        {
            mediaPlayer.close();
            mediaPlayer = null;
            player = null;
        }
            

    }
}

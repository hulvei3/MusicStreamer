using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicStreamer.Exceptions;
using MusicStreamer.Models.Server;
using WMPLib;
using System.Net;

namespace MusicStreamer.ViewModels.Streamer
{
    class StreamerViewModel : PropertyAndErrorHandler
    {
        private ServerConnectionModel _scm;

        private WebClient _streamClient;

        public StreamerViewModel(ServerConnectionModel scm)
        {
            _scm = scm;

            _streamClient = new WebClient();

            _streamClient.DownloadProgressChanged += DownloadProgressChanged;

            
        }
        // returns true if streaming was started
        public bool StreamMedia(string url)
        {

            if (_streamClient.IsBusy)
                return false;

            _streamClient.DownloadFileAsync(new Uri(url),"temp_music");


            
            return true;
        }

        public event DownloadProgressChangedEventHandler DownloadProgressChanged;
    }
}

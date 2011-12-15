using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicStreamer.Exceptions;
//using MusicStreamer.Models.Server;
using WMPLib;
using System.Net;
using MusicStreamer.Util;
using StreamerLib;

namespace MusicStreamer.ViewModels.Streamer
{
    class StreamerViewModel : PropertyAndErrorHandler
    {
        private ServerConnectionModel _scm;

        private WebClient _streamClient;

        public StreamerViewModel()
        {
            //_scm = scm;

            _streamClient = new WebClient();
            // TODO
            var model = new ConnectionLibrary().Connections.First();

            _streamClient.Credentials = new NetworkCredential(model.User, model.Password);
            _streamClient.DownloadProgressChanged += DownloadProgressChanged;

        }
        // returns true if streaming was started
        public string StreamMedia(string url)
        {
            

            var localfile = string.Format("{0}.mp3",Guid.NewGuid().ToString());

            if (_streamClient.IsBusy)
                throw new StreamingInProgressException("Streaming already in progress\nMusic Streamer doesn't support multistreamning.");
            try
            {
                //_streamClient.Proxy = null;
                _streamClient.DownloadFileAsync(new Uri(url), localfile);
            }
            catch (WebException)
            {
                throw new MusicStreamerException("Download error");
            }
            
            return localfile;
        }

        public event DownloadProgressChangedEventHandler DownloadProgressChanged;
    }
}

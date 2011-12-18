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
using System.ComponentModel;
using System.Windows.Input;

namespace MusicStreamer.ViewModels.Streamer
{
    public class StreamerViewModel : PropertyAndErrorHandler
    {
        private ServerConnectionModel _scm;
        private WebClient _streamClient;

        private Status _streamerStatus;

        public Status StreamerStatus
        {
            get { return _streamerStatus; }
            set
            { 
                _streamerStatus = value;
                OnPropertyChanged("StreamerStatus");
            }
        }

        public StreamerViewModel()
        {

            var model = MainWindowViewModel.Instance.Navigation.CurrentServer;

            if (model == null)
                return;
            
            _streamClient = new WebClient();
            
            

            _streamClient.Credentials = new NetworkCredential(model.User, model.Password);
            _streamClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(_streamClient_DownloadProgressChanged);
            _streamClient.DownloadFileCompleted += new AsyncCompletedEventHandler(_streamClient_DownloadFileCompleted);
            
            StreamerStatus = Status.Idle;
        }
        
        // returns true if streaming was started
        public string StreamMedia(string url)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            var localfile = string.Format("{0}.mp3",Guid.NewGuid().ToString());

            if (_streamClient.IsBusy)
                throw new StreamingInProgressException("Streaming already in progress\nMusic Streamer doesn't support multistreamning.");
            try
            {
                _streamClient.Proxy = null;
                _streamClient.DownloadFileAsync(new Uri(url), localfile);
                StreamerStatus = Status.Streaming;
            }
            catch (WebException)
            {
                throw new MusicStreamerException("Streamer error");
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }


            return localfile;
        }

        public event DownloadProgressChangedEventHandler DownloadProgressChanged;

        void _streamClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            StreamerStatus = Status.Streaming;

            DownloadProgressChangedEventHandler handler = DownloadProgressChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event AsyncCompletedEventHandler DownloadCompleted;

        void _streamClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            StreamerStatus = Status.Idle;

            AsyncCompletedEventHandler handler = DownloadCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

    }

    public enum Status
    {
        Streaming,
        Idle
    }
}

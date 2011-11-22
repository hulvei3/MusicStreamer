using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using MusicStreamer.Models.Server;
using System.Windows.Input;
using System.Windows;

namespace MusicStreamer.ViewModels.Server
{
    class ServerConnectionViewModel : Exceptions.PropertyAndErrorHandler
    {
        private ServerConnectionModel _model;
  

        public ServerConnectionViewModel(ServerConnectionModel _model)
        {
            this._model = _model;          
            NewURL(_model.Host);   
        }

        private FtpWebRequest Request
        {
            get;
            set;
        }

        public void NewURL(String url)
        {
            Request = (FtpWebRequest)WebRequest.Create(url);
            Request.Credentials = new NetworkCredential(_model.User, _model.Password);
            Request.Proxy = null;

        }

        public FtpWebResponse ListCurrentDir()
        {
            FtpWebResponse result = null;
            Request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            
            try
            {
                FtpWebResponse response = (FtpWebResponse)Request.GetResponse();
                result = response;

            }
            catch (WebException e)
            {
                //MessageBox.Show(e.Message);
                result = null;
            }

            return result;
        }

        //Tilføj metoder som håndterer download af sang etc.

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
//using MusicStreamer.Models.Server;
using System.Windows.Input;
using System.Windows;
using MusicStreamer.Exceptions;
using DLLs;

namespace MusicStreamer.ViewModels.Server
{
    class ServerConnectionViewModel : Exceptions.MusicStreamerException
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
            try
            {
                Request = (FtpWebRequest)WebRequest.Create(url);
                Request.Credentials = new NetworkCredential(_model.User, _model.Password);
                Request.Proxy = null;
            }
            catch (UriFormatException e)
            {
                return;
            }

        }

        public FtpWebResponse ListCurrentDir()
        {
            FtpWebResponse result = null;
            try
            {
                Request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            }
            catch (InvalidOperationException e)
            {
                throw new MusicStreamerException("URL not valid.");
            }
            
            try
            {
                FtpWebResponse response = (FtpWebResponse)Request.GetResponse();
                result = response;
            }
            catch (WebException e)
            {
                
                throw new MusicStreamerException("Tried to connect to unreachable host.");
                
            }
            return result;
        }

        //Tilføj metoder som håndterer download af sang etc.

    }
}

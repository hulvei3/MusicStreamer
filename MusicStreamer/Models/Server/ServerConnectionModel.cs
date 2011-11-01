using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicStreamer.Models.Server
{
    class ServerConnectionModel
    {   
        // model for en server connection. hhv brugernavn, passw. og ftp ip

        private String _hostIP;

        public ServerConnectionModel(String hostIP, String userName, String userPassword)
        {
            this._hostIP = hostIP;
            User = userName;
            Password = userPassword;
        }

        public String Host
        {
            get { return _hostIP; }
            set { _hostIP = "ftp://" + value; }
        }

        public String User
        {
            get;
            private set;
        }

        public String Password
        {
            get;
            private set;
        }
    }
}

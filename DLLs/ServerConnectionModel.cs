using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreamerLib
{
    public class ServerConnectionModel
    {   
        // model for en server connection. hhv brugernavn, passw. og ftp ip

        private String _hostIP;

        public ServerConnectionModel(String hostIP, String userName, String userPassword)
        {
            Host = hostIP;
            User = userName;
            Password = userPassword;

                Host.TrimEnd('/');
        }

        public String Host
        {
            get { return _hostIP; }
            set { _hostIP = value.StartsWith("ftp://") ? value : "ftp://" + value; }
        }

        public String User
        {
            get;
            set;
        }

        public String Password
        {
            get;
            set;
        }
    }
}

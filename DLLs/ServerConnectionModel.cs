using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreamerLib
{
    public class ServerConnectionModel
    {   
        // model for en server connection. hhv brugernavn, passw. og ftp ip

        public String _hostIP;

        public ServerConnectionModel(String hostIP, String userName, String userPassword)
        {
            Host = hostIP;
            User = userName;
            Password = userPassword;
        }

        public String Host
        {
            get { return _hostIP; }
            private set { _hostIP = "ftp://" + value; }
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

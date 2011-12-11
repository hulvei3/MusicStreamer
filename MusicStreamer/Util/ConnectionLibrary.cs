using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
//using MusicStreamer.Models.Server;
using DLLs;

namespace MusicStreamer.Util
{
    class ConnectionLibrary
    {

        public ObservableCollection<ServerConnectionModel> Connections { get; private set; }

        public ConnectionLibrary()
        {
            Init();
            AddConnection(new ServerConnectionModel("90.184.75.15", "streamer", "streamerpassword"));
        }

        private void Init()
        {
            Connections = new ObservableCollection<ServerConnectionModel>();

            //TODO: load connections from file here
        }

        public void AddConnection(ServerConnectionModel newModel)
        {
            Connections.Add(newModel);
        }
    }
}

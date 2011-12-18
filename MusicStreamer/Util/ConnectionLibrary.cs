using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
//using MusicStreamer.Models.Server;
using StreamerLib;
using MusicStreamer.Exceptions;

namespace MusicStreamer.Util
{
    public class ConnectionLibrary : PropertyAndErrorHandler
    {
        private ObservableCollection<ServerConnectionModel> _connections;
        public ObservableCollection<ServerConnectionModel> Connections
        {
            get { return _connections; }
            private set
            {
                _connections = value;
                OnPropertyChanged("Connections");
            }
        }

        public ConnectionLibrary()
        {
            Init();
            AddConnection(new ServerConnectionModel("90.184.75.15", "streamer", "streamerpassword"));
            AddConnection(new ServerConnectionModel("jpics.dk", "Harving", "Harving2011"));
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
        public void RemoveConnection(ServerConnectionModel model)
        {
            Connections.Remove(model);
        }
        public void UpdateConnection(ServerConnectionModel old, ServerConnectionModel @new)
        {
            var model = Connections.Single<ServerConnectionModel>(x => x.Equals(old));
            model.Host = @new.Host;
            model.User = @new.User;
            model.Password = @new.Password;
        }

        public ServerConnectionModel Current { get; set; }
    }
}

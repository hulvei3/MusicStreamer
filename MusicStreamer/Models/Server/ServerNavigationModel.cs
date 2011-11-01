using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicStreamer.Models.Server
{
    // klasse der indeholder oplysninger om nuværende lokation 
    class ServerNavigationModel
    {

        private String _currentLocation;

        public ServerNavigationModel(String currentLocation)
        {
            this._currentLocation = currentLocation; 
        }

        public String CurrentLocation
        {
            get;
            set;
        }
    }
}

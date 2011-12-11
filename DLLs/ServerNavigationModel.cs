using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLs
{
    // klasse der indeholder oplysninger om nuværende lokation 
    public class ServerNavigationModel
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

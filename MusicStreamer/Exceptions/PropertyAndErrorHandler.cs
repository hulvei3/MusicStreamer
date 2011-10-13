using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MusicStreamer.Exceptions
{
    // This class can be used as global handler to propertynotification and errorhandling
    public abstract class PropertyAndErrorHandler : INotifyPropertyChanged
    {
        private string _debugText = "ready";

        [Obsolete]
        private static string _ON_PROPERTY_CHANGED_STRING_NOTIFICATION;

        public string DebugText
        {
            get { return _debugText; }
            set
            {
                _debugText = value;
                OnPropertyChanged("DebugText");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

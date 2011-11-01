using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace MusicStreamer.Exceptions
{
    // This class can be used as global handler to propertynotification and errorhandling
    public abstract class PropertyAndErrorHandler : DependencyObject, INotifyPropertyChanged
    {
        private string _debugText = "Ready";

        public const string _STD_DEBUG_TEXT = "Ready";

        public string DebugText
        {
            get { return _debugText; }
            set
            {
                _debugText = value.Length == 0 ? _STD_DEBUG_TEXT : value;
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

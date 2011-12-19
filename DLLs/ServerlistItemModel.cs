using System.ComponentModel;
using System.Windows.Input;

namespace StreamerLib
{
    public class ServerlistItemModel : INotifyPropertyChanged
    {
        private string _url;
        private string _size;

        public ServerlistItemModel(string name, string size)
        {
            Url = name;
            Size = size;
            Name = name;
        }
        public string Name { get; set; }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }
        public RoutedCommand AddCommand{ get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

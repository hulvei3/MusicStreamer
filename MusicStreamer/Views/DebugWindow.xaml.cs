using System.Windows;
using MusicStreamer.CustomCommands;

namespace MusicStreamer.Views
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        public DebugWindow(MainWindow vm)
        {
            this.DataContext = vm;

            

            InitializeComponent();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            //((MainWindow)DataContext).buttonDebug.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}

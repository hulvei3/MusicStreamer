using System.Windows;

using MusicStreamer.ViewModels;
using MusicStreamer.Views;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicStreamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly MainWindowViewModel _vm;

        public MainWindow()
        {
            _vm = new MainWindowViewModel();
            this.DataContext = _vm;
            InitializeComponent();
        }

        // this is only here for testing

        private void buttonRepeat_Click(object sender, RoutedEventArgs e)
        {
            //textBoxCurrentSong.Text = @"C:\Users\Morten Hulvej\Desktop\07 I Remember.mp3";


            _vm.Playlist.OpenTestPlaylist();
        }

        private void buttonShuffle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void windowMain_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.WindowUI = this;

            // setting commands to buttons
            buttonConnect.Command = _vm.CommandLib.ConnectCommand;
            //((Button)(serverlistBox.ItemTemplate.VisualTree.FirstChild.NextSibling.NextSibling.GetType())).Command = _vm.CommandLib.AddToPlaylistCommand;
            
        }

        private void buttonDebug_Click(object sender, RoutedEventArgs e)
        {
            
            new DebugWindow(this).Show();
            buttonDebug.IsEnabled = false;
        }

        //test
        private void currentLocationTextBox_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string newUrl = currentLocationTextBox.Text.ToString();
                _vm.Navigation.CurrentLocation = newUrl;
                currentLocationTextBox.CaretIndex = newUrl.Length+1;
                _vm.Navigation.Navigate();
            }
        }
    }
}

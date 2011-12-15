using System.Windows;

using MusicStreamer.ViewModels;
using MusicStreamer.Views;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        private void windowMain_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.CommandLib.UIParent = this;
            
            // setting commands to buttons
            buttonConnect.Command = _vm.CommandLib.ConnectCommand;
            buttonPlay.Command = _vm.CommandLib.PlayPauseCommand;
            buttonNext.Command = _vm.CommandLib.NextCommand;
            buttonPrev.Command = _vm.CommandLib.PrevCommand;
            buttonStop.Command = _vm.CommandLib.StopCommand;
            buttonShuffle.Command = _vm.CommandLib.ShuffleCommand;
            buttonRepeat.Command = _vm.CommandLib.RepeatCommand;
            
            
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
                currentLocationTextBox.CaretIndex = currentLocationTextBox.Text.Length;
                _vm.Navigation.Navigate();
            }
        }

    }
}

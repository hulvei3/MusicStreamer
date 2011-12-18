using System.Windows;

using MusicStreamer.ViewModels;
using MusicStreamer.Views;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MusicStreamer.CustomCommands;

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

        private void windowMain_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.CommandLib.UIParent = this;
            
            // setting commands to buttons
            //buttonConnect.Command = _vm.CommandLib.ConnectCommand;
            buttonPlay.Command = _vm.CommandLib.PlayPauseCommand;
            buttonNext.Command = _vm.CommandLib.SkipCommand;
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

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            DeactivatePlayerInterface();
        }

        private void DeactivatePlayerInterface()
        {

        }

        private void ConnectionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenConnectionWindow();
        }

        private void buttonConnect_Click(object sender, RoutedEventArgs e)
        {
            OpenConnectionWindow();
        }

        private void OpenConnectionWindow()
        {
            var window = new ConnectionWindow();
            window.ShowDialog();
        }

        private void ServerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServerComboBox.SelectedIndex == -1)
                DefaultComboTextBlock.Visibility = System.Windows.Visibility.Visible;
            else
                DefaultComboTextBlock.Visibility = System.Windows.Visibility.Hidden;
        }


    }
}

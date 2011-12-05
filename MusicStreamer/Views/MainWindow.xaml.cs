using System.Windows;

using MusicStreamer.ViewModels;

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

        //private void buttonConnect_Click(object sender, RoutedEventArgs e)
        //{
        //    // TEST

        //    textBoxCurrentSong.Text = @"C:\Users\Morten Hulvej\Desktop\07 I Remember.mp3";


        //}

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
        }
    }
}

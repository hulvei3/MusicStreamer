using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MusicStreamer.Exceptions;
using MusicStreamer.ViewModels;
using System.Collections.ObjectModel;
using MusicStreamer.ViewModels.Server;
using MusicStreamer.ViewModels.Playlist;

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


            _vm.Playlist.addNewMedia();
        }

        private void buttonShuffle_Click(object sender, RoutedEventArgs e)
        {
            _vm.Playlist.CurrentPlaylist.Clear();
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

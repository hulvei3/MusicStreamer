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

namespace MusicStreamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<ServerlistItemViewModel> list;

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


            //_vm.Navigation.add_to_list_test();

            //list = new ObservableCollection<ServerlistItemViewModel>();

            //serverlistBox.ItemsSource = list;
        }

        private void buttonShuffle_Click(object sender, RoutedEventArgs e)
        {
            _vm.Playlist.CurrentPlaylist.Clear();
        }

        private void buttonPrev_Click(object sender, RoutedEventArgs e)
        {
            //list.Add(new ServerlistItemViewModel("din mor",2.0));
        }

        //private void serverlistBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    _vm.Navigation.Navigate("OpenShare");
        //}






        //private void buttonPlay_Click(object sender, RoutedEventArgs e)
        //{
        //    PlayerEngineModel.instance.loadFile("C:\\Users\\Morten Hulvej\\Desktop\\07 I Remember.mp3");

        //    PlayerEngineModel.instance.playFile();


        //}

        //private void buttonStop_Click(object sender, RoutedEventArgs e)
        //{
        //    PlayerEngineModel.instance.stopFile();

        //}

        //private void buttonConnect_Click(object sender, RoutedEventArgs e)
        //{
        //    int time = (int)PlayerEngineModel.instance.CurrentTime;


        //    int hours = time / 3600;
        //    int minutes = time / 60;
        //    int seconds = time % 60;

        //    StringBuilder sb = new StringBuilder();

        //    sb.Append( (hours < 10) ? "0" + hours : hours.ToString() );
        //    sb.Append(":");
        //    sb.Append( (minutes < 10) ? "0" + minutes : minutes.ToString() );
        //    sb.Append(":");
        //    sb.Append((seconds < 10) ? "0" + seconds : seconds.ToString() );

        //    textBoxTime.Text = sb.ToString();
        //    //textBoxTime.Text = time / 3600 + ":" + time / 60 + ":" + time % 60;
        //}
    }
}

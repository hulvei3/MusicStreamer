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
<<<<<<< HEAD
        //private void buttonConnect_Click(object sender, RoutedEventArgs e)
        //{
        //    // TEST

        //    textBoxCurrentSong.Text = @"C:\Users\Morten Hulvej\Desktop\07 I Remember.mp3";


        //}
=======
        private void buttonRepeat_Click(object sender, RoutedEventArgs e)
        {
            textBoxCurrentSong.Text = @"C:\Users\Morten Hulvej\Desktop\07 I Remember.mp3";
        }


>>>>>>> 2c942b0a8a62852b2c22555fcfe47b2b78366d70

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

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

namespace MusicStreamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            PlayerEngine.instance.loadFile("C:\\Users\\Morten Hulvej\\Desktop\\07 I Remember.mp3");

            PlayerEngine.instance.playFile();
            
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            PlayerEngine.instance.stopFile();

        }

        private void buttonConnect_Click(object sender, RoutedEventArgs e)
        {
            int time = (int)PlayerEngine.instance.CurrentTime;


            int hours = time / 3600;
            int minutes = time / 60;
            int seconds = time % 60;

            StringBuilder sb = new StringBuilder();

            sb.Append( (hours < 10) ? "0" + hours : hours.ToString() );
            sb.Append(":");
            sb.Append( (minutes < 10) ? "0" + minutes : minutes.ToString() );
            sb.Append(":");
            sb.Append((seconds < 10) ? "0" + seconds : seconds.ToString() );

            textBoxTime.Text = sb.ToString();
            //textBoxTime.Text = time / 3600 + ":" + time / 60 + ":" + time % 60;
        }
    }
}

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

        private void buttonShuffle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void windowMain_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.CommandLib.UIParent = this;

            // setting commands to buttons
            buttonConnect.Command = _vm.CommandLib.ConnectCommand;
            //var tempParent = serverlistBox.TemplatedParent;
            ////((Button)(serverlistBox.ItemTemplate.FindName("buttonAddToPlaylist",tempParent)));

            //ListBoxItem serverListBoxItem =
            //(ListBoxItem)(serverlistBox.ItemContainerGenerator.ContainerFromItem(serverlistBox.Items.CurrentItem));

            //// Getting the ContentPresenter of myListBoxItem
            //ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(serverListBoxItem);

            //// Finding textBlock from the DataTemplate that is set on that ContentPresenter
            //DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            //Button addButton = (Button)myDataTemplate.FindName("buttonAddToPlaylist", myContentPresenter);

            //// Do something to the DataTemplate-generated TextBlock
            //addButton.Command = _vm.CommandLib.AddToPlaylistCommand;
            
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
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

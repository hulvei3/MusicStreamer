using System.Windows;
using MusicStreamer.ViewModels;
using StreamerLib;
using System.Windows.Input;
using MusicStreamer.CustomCommands;

namespace MusicStreamer.Views
{
    /// <summary>
    /// Interaction logic for ConnectionWindow.xaml
    /// </summary>
    partial class ConnectionWindow : Window
    {
        public ConnectionWindow()
        {
            this.DataContext = MainWindowViewModel.Instance;
            InitializeComponent();

            var cmdBind = new CommandBinding(MainWindowViewModel.Instance.CommandLib.ConnectCommand,
                    (e2, args) =>
                    {
                        new ConnectCommand(null).Execute(SelectedServer);
                        this.Close();
                    },
                    (e3, args) =>
                    {
                        args.CanExecute = true;
                    });
            CommandBindings.Add(cmdBind);
            Connectbutton.Command = MainWindowViewModel.Instance.CommandLib.ConnectCommand;
        }

        public ServerConnectionModel SelectedServer { get; set; }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newServer = new ServerConnectionModel("new", "", "");
            MainWindowViewModel.Instance.Servers.AddConnection(newServer);
            SelectedServer = newServer;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.Instance.Servers.RemoveConnection(SelectedServer);
        }

        private void ServerListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var server = (ServerConnectionModel)ServerListBox.SelectedItem;

            var sel = new string[3];

            HostTextBox.Text = sel[0] = server.Host;
            UserTextBox.Text = sel[1] = server.User;
            PasswordTextBox.Password = sel[2] = server.Password;

            SelectedServer = server;
        }

        private void OKbutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}

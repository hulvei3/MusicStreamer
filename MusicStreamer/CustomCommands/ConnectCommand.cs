using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;
using MusicStreamer.ViewModels.Server;
using MusicStreamer.Models.Server;
using System.Windows;

namespace MusicStreamer.CustomCommands
{
    class ConnectCommand : ICommand
    {
        MainWindowViewModel _mwvm;
      
        //Lav konstruktør som hente mainwindowviewmodel ind
        public ConnectCommand(MainWindowViewModel mwvm)
        {
            this._mwvm = mwvm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            //Skal have username, pw og host url ind fra MainWindow.xaml
           
            String[] info = (String[])parameter;
            //MessageBox.Show(info[0] + info[1] + info[2]);
            _mwvm.Navigation = new ServerNavigationViewModel(new ServerConnectionModel(info[0], info[1], info[2]));
            _mwvm.Navigation.Navigate();

            //StringBuilder sList = new StringBuilder();
            //foreach (var item in _mwvm.Navigation.Navigate())
            //{
            //    sList.AppendLine(item);
            //}
           
            //MessageBox.Show(sList.ToString());
            
            //Skal vise roden af ftp serveren her
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MusicStreamer.CustomCommands
{
    class ConnectCommand : ICommand
    {
        //Lav konstruktør som hente mainwindowviewmodel ind


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            //Skal have username, pw og host url ind fra MainWindow.xaml
            //Skal vise roden af ftp serveren
            throw new NotImplementedException();
        }
    }
}

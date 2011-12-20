using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;
using MusicStreamer.ViewModels.Server;
//using MusicStreamer.Models.Server;
using System.Windows;
using MusicStreamer.Interfaces;
using StreamerLib;

namespace MusicStreamer.CustomCommands
{
    class ConnectCommand : IBaseCommand
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
           
            var model = (ServerConnectionModel)parameter;
            new Action(() =>
                {
                    MainWindowViewModel.Instance.Navigation.setConnectionModel(model);
                    MainWindowViewModel.Instance.Navigation.Navigate();
                    MainWindowViewModel.Instance.CurrentSong.SetupStreamer();
                }).BeginInvoke(null, null);
            
        }

        public void UnExecute()
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            //bruger overload, pga parametrene
        }
    }
}

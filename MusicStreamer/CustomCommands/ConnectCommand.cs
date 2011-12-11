﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.ViewModels;
using MusicStreamer.ViewModels.Server;
//using MusicStreamer.Models.Server;
using System.Windows;
using MusicStreamer.Interfaces;
using DLLs;

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
            //Skal have username, pw og host url ind fra MainWindow.xaml
           
            String[] info = (String[])parameter;
            //MessageBox.Show(info[0] + info[1] + info[2]);

            // TODO: var model = new ConnectionLibrary().Connections.First();
            if (info[0].EndsWith("/"))
            {
                info[0] = info[0].Remove(info[0].Length - 1);
            }

            _mwvm.Navigation.setConnectionModel(new ServerConnectionModel(info[0], info[1], info[2]));
            _mwvm.Navigation.Navigate();



            //StringBuilder sList = new StringBuilder();
            //foreach (var item in _mwvm.Navigation.Navigate())
            //{
            //    sList.AppendLine(item);
            //}
           
            //MessageBox.Show(sList.ToString());
            
            //Skal vise roden af ftp serveren her
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

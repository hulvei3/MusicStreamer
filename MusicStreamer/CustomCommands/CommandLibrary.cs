﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicStreamer.ViewModels;
using System.Windows.Input;
using System.Windows;

namespace MusicStreamer.CustomCommands
{
    public class CommandLibrary
    {
        private readonly Window _UIParent;
        private readonly UndoRedoController _undoRedoController = UndoRedoController.Instance;

        // bliver ikke brugt endnu..
        public IList<ICommand> CustomCommands { get; set; }


        public RoutedCommand ConnectCommand = new RoutedCommand();
        public RoutedCommand AddToPlaylistCommand = new RoutedCommand();
        //public RoutedCommand SavePlaylistCommand = new RoutedCommand();
        public RoutedCommand LoadPlaylistCommand = new RoutedCommand();

        public RoutedCommand NavigateCommand = new RoutedCommand();

        public RoutedCommand PlayPauseCommand = new RoutedCommand();
        public RoutedCommand PrevCommand = new RoutedCommand();
        public RoutedCommand SkipCommand = new RoutedCommand();
        public RoutedCommand StopCommand = new RoutedCommand();

        public CommandLibrary(Window ui)
        {
            _UIParent = ui;

            //CustomCommands = new List<ICommand>();

            InitCustomCommands();
            InitAppCommands();
        }

        private void InitCustomCommands()
        {
            //Her initialiseres alle vores egne Commands..


            // connect
            var cbinding = new CommandBinding(ConnectCommand, ConnectExecute, ConnectCanExecute);
            _UIParent.CommandBindings.Add(cbinding);

            // navigate
            //cmd = new NavigateCommand(MainWindowViewModel.Instance);
            //cbinding = new CommandBinding(cmd, cmd.Execute, cmd.CanExecute);
            //_UIParent.CommandBindings.Add(cbinding);

            //// playPause
            //cmd = new PlayPauseCommand(MainWindowViewModel.Instance.CurrentSong);
            //cbinding = new CommandBinding(cmd, cmd.Execute, cmd.CanExecute);
            //_UIParent.CommandBindings.Add(cbinding);

        }

        // ApplicationCommands (indbygget i en WPF-applikation)
        private void InitAppCommands()
        {
            var cbinding = new CommandBinding(ApplicationCommands.Undo, _undoRedoController.Undo,
                                          _undoRedoController.CanExecuteUndo);
            _UIParent.CommandBindings.Add(cbinding);

            cbinding = new CommandBinding(ApplicationCommands.Redo, _undoRedoController.Redo,
                                          _undoRedoController.CanExecuteRedo);
            _UIParent.CommandBindings.Add(cbinding);

            cbinding = new CommandBinding(ApplicationCommands.SaveAs, SavePlaylistExecute, SavePlaylistCanExecute);

            _UIParent.CommandBindings.Add(cbinding);
        }


        private void ConnectExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = new ConnectCommand(MainWindowViewModel.Instance);
            cmd.Execute(e.Parameter);
            _undoRedoController.PushUndoStack(cmd);
        }
        private void ConnectCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SavePlaylistExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = new SavePlaylistCommand(MainWindowViewModel.Instance.Playlist);
            cmd.Execute();
        }
        private void SavePlaylistCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}

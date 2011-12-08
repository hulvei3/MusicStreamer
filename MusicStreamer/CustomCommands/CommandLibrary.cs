using System;
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

        public UndoRedoController UndoRedoController { get; private set; }

        public RoutedCommand ConnectCommand = new RoutedCommand();

        public RoutedCommand AddToPlaylistCommand = new RoutedCommand();

        public RoutedCommand LoadPlaylistCommand = new RoutedCommand();

        public RoutedCommand NavigateCommand = new RoutedCommand();

        public RoutedCommand PlayPauseCommand = new RoutedCommand();
        public RoutedCommand PrevCommand = new RoutedCommand();
        public RoutedCommand SkipCommand = new RoutedCommand();
        public RoutedCommand StopCommand = new RoutedCommand();

        public CommandLibrary(Window ui)
        {
            _UIParent = ui;
            UndoRedoController = _undoRedoController;

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
            cbinding = new CommandBinding(NavigateCommand, NavigateExecute, NavigateCanExecute);
            _UIParent.CommandBindings.Add(cbinding);

            // addToPlaylist
            cbinding = new CommandBinding(AddToPlaylistCommand, AddToPlaylistExecute, AddToPlaylistCanExecute);
            _UIParent.CommandBindings.Add(cbinding);

            ////// playPause
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
            //_undoRedoController.PushUndoStack(cmd);   //Connect should not be undo-able
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

        private void AddToPlaylistExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = new AddToPlaylistCommand(MainWindowViewModel.Instance);
            cmd.Execute(e.Parameter);
            _undoRedoController.PushUndoStack(cmd);
        }
        private void AddToPlaylistCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NavigateExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = new NavigateCommand(MainWindowViewModel.Instance);
            cmd.Execute(e.Parameter);
            _undoRedoController.PushUndoStack(cmd);
        }
        private void NavigateCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}

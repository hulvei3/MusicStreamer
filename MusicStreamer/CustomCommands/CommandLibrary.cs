using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicStreamer.ViewModels;
using System.Windows.Input;
using System.Windows;
using MusicStreamer.Exceptions;

namespace MusicStreamer.CustomCommands
{
    public class CommandLibrary
    {
        private readonly UndoRedoController _undoRedoController = UndoRedoController.Instance;
        private Window _UIParent;

        public Window UIParent
        {
            get { return _UIParent; }
            set { _UIParent = _UIParent ?? value;
            InitCustomCommands();
            InitAppCommands();
            }
        }


        // bliver ikke brugt endnu..
        //public IList<ICommand> CustomCommands { get; set; }

        public UndoRedoController UndoRedoController { get; private set; }

        public RoutedCommand ConnectCommand = new RoutedCommand();

        public RoutedCommand AddToPlaylistCommand = new RoutedCommand();

        public RoutedCommand LoadPlaylistCommand = new RoutedCommand();

        public RoutedCommand NavigateCommand = new RoutedCommand();

        public RoutedCommand PlayPauseCommand = new RoutedCommand();
        public RoutedCommand PrevCommand = new RoutedCommand();
        public RoutedCommand SkipCommand = new RoutedCommand();
        public RoutedCommand StopCommand = new RoutedCommand();

        public CommandLibrary()
        {
            UndoRedoController = _undoRedoController;
        }

        public CommandLibrary(Window ui)
        {
            _UIParent = ui;
            UndoRedoController = _undoRedoController;
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

            cbinding = new CommandBinding(ApplicationCommands.Save, SavePlaylistExecute, SavePlaylistCanExecute);
            _UIParent.CommandBindings.Add(cbinding);

            cbinding = new CommandBinding(ApplicationCommands.Open, LoadPlaylistExecute, LoadPlaylistCanExecute);
            _UIParent.CommandBindings.Add(cbinding);

            cbinding = new CommandBinding(ApplicationCommands.Close, CloseExecute, CloseCanExecute);
            _UIParent.CommandBindings.Add(cbinding);

            cbinding = new CommandBinding(ApplicationCommands.Help, HelpExecute, HelpCanExecute);
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
            cmd.Execute(null);
        }
        private void SavePlaylistCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddToPlaylistExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = new AddToPlaylistCommand(MainWindowViewModel.Instance);
            try
            {
                cmd.Execute(e.Parameter);
            }
            catch (PlaylistException ex)
            {
                return;
            }
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
        private void LoadPlaylistExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = new LoadPlaylistCommand(MainWindowViewModel.Instance.Playlist);
            cmd.Execute(null);
        }

        private void LoadPlaylistCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CloselistExecute(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Bye");
            Application.Current.Shutdown();
        }
        private void CloseCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void HelpExecute(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Help");
        }
        private void HelpCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}

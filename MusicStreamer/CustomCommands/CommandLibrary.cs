﻿using System.Windows;
using System.Windows.Input;
using MusicStreamer.Exceptions;
using MusicStreamer.ViewModels;
using MusicStreamer.Controls;
using System.IO;

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

        public UndoRedoController UndoRedoController { get; private set; }

        public RoutedCommand ConnectCommand = new RoutedCommand();

        public RoutedCommand AddToPlaylistCommand = new RoutedCommand();
        public RoutedCommand RemoveFromPlaylistCommand = new RoutedCommand();

        public RoutedCommand LoadPlaylistCommand = new RoutedCommand();

        public RoutedCommand NavigateCommand = new RoutedCommand();

        public RoutedCommand PlayPauseCommand = new RoutedCommand();
        public RoutedCommand PrevCommand = new RoutedCommand();
        public RoutedCommand SkipCommand = new RoutedCommand();
        public RoutedCommand StopCommand = new RoutedCommand();
        public RoutedCommand ShuffleCommand = new RoutedCommand();
        public RoutedCommand RepeatCommand = new RoutedCommand();

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

            cbinding = new CommandBinding(RemoveFromPlaylistCommand, RemoveFromPlaylistExecute, RemoveFromPlaylistCanExecute);
            _UIParent.CommandBindings.Add(cbinding);
            
            // playPause
            cbinding = new CommandBinding(PlayPauseCommand, PlayPauseExecute, PlayPauseCanExecute);
            _UIParent.CommandBindings.Add(cbinding);

            // skip
            cbinding = new CommandBinding(SkipCommand, SkipExecute, SkipCanExecute);
            _UIParent.CommandBindings.Add(cbinding);

            // prev
            cbinding = new CommandBinding(PrevCommand, PrevExecute, PrevCanExecute);
            _UIParent.CommandBindings.Add(cbinding);

            // stop
            cbinding = new CommandBinding(StopCommand, StopExecute, StopCanExecute);
            _UIParent.CommandBindings.Add(cbinding);

            // repeat
            cbinding = new CommandBinding(RepeatCommand, RepeatExecute, RepeatCanExecute);
            _UIParent.CommandBindings.Add(cbinding);

            // shuffle
            cbinding = new CommandBinding(ShuffleCommand, ShuffleExecute, ShuffleCanExecute);
            _UIParent.CommandBindings.Add(cbinding);
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

            cbinding = new CommandBinding(ApplicationCommands.New, NewExecute, NewCanExecute);
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
        private void RemoveFromPlaylistExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = new RemoveFromPlaylistCommand(MainWindowViewModel.Instance);
            cmd.Execute(e.Parameter);
            _undoRedoController.PushUndoStack(cmd);
        }

        private void RemoveFromPlaylistCanExecute(object sender, CanExecuteRoutedEventArgs e)
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

        
        #region Playercontrols
        private void PlayPauseExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = new PlayPauseCommand(MainWindowViewModel.Instance);
            cmd.Execute(e.Parameter);
        }
        private void PlayPauseCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void SkipExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = new SkipCommand(MainWindowViewModel.Instance.CurrentSong);
            cmd.Execute();
        }
        private void SkipCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void PrevExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = new PrevCommand(MainWindowViewModel.Instance.CurrentSong);
            cmd.Execute();
        }
        private void PrevCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void StopExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = new StopCommand(MainWindowViewModel.Instance.CurrentSong);
            cmd.Execute();
        }
        private void StopCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void RepeatExecute(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void RepeatCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ShuffleExecute(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void ShuffleCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region AppCommands handlers

        private void CloseExecute(object sender, ExecutedRoutedEventArgs e)
        {
            StopCommand.Execute(null, null);
            //MessageBox.Show("Bye");
            
            //var di = new DirectoryInfo("tempdata");

            //foreach (var file in di.GetFiles("*", SearchOption.AllDirectories))
            //    file.Attributes = FileAttributes.ReadOnly;
            try
            {
                Directory.Delete("tempdata", true);
            }
            catch (System.Exception)
            {
            }

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
        private void LoadPlaylistExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = new LoadPlaylistCommand(MainWindowViewModel.Instance.Playlist);
            cmd.Execute(null);
        }

        private void LoadPlaylistCanExecute(object sender, CanExecuteRoutedEventArgs e)
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
        private void NewExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = new ClearPlaylistCommand(MainWindowViewModel.Instance);
            cmd.Execute(null);
        }
        private void NewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion
    }
}

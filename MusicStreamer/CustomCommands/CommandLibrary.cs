using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicStreamer.ViewModels;
using System.Windows.Input;
using System.Windows;

namespace MusicStreamer.CustomCommands
{
    class CommandLibrary
    {
        private readonly Window _parent;
        private readonly UndoRedoController _undoRedoController = UndoRedoController.Instance;

        public CommandLibrary(Window ui)
        {
            _parent = ui;

            CustomCommands = new List<ICommand>();

            InitCustomCommands();
            InitAppCommands();
        }

        private void InitCustomCommands()
        {
             //Her initialiseres alle vores egne Commands..

            var cmd = new ConnectCommand(_parent);

            

            var cbinding = new CommandBinding( cmd, cmd.Execute, cmd.CanExecute);
                _parent.CommandBindings.Add(cbinding);

            
        }

        // ApplicationCommands (indbygget i en WPF-applikation)
        private void InitAppCommands()
        {
            cbinding = new CommandBinding(ApplicationCommands.Undo, _undoRedoController.Undo,
                                          _undoRedoController.CanExecuteUndo);
            _parent.CommandBindings.Add(cbinding);

            cbinding = new CommandBinding(ApplicationCommands.Redo, _undoRedoController.Redo,
                                          _undoRedoController.CanExecuteRedo);
            _parent.CommandBindings.Add(cbinding);
        }

        public IList<ICommand> CustomCommands { get; set; }
    }
}

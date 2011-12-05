using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicStreamer.ViewModels;
using System.Windows.Input;

namespace MusicStreamer.CustomCommands
{
    class CommandLibrary
    {
        private readonly MainWindowViewModel _parent;
        private readonly UndoRedoController _undoRedoController = UndoRedoController.Instance;

        public IList<ICommand> CustomCommands = new List<ICommand>();

        public CommandLibrary(MainWindowViewModel parent)
        {
            _parent = parent;

            InitCustomCommands();
            InitAppCommands();
        }

        public void InitCustomCommands()
        {
             //Her initialiseres alle vores egne Commands..

            var cbinding = new CommandBinding(AddEllipse, AddEllipseExecute, AddEllipseCanExecute);
                _parent.CommandBindings.Add(cbinding);

            
        }

        // ApplicationCommands (indbygget i en WPF-applikation)
        public void InitAppCommands()
        {
            #region ApplicationCommands

            cbinding = new CommandBinding(ApplicationCommands.Undo, _undoRedoController.Undo,
                                          _undoRedoController.CanExecuteUndo);
            _parent.CommandBindings.Add(cbinding);

            cbinding = new CommandBinding(ApplicationCommands.Redo, _undoRedoController.Redo,
                                          _undoRedoController.CanExecuteRedo);
            _parent.CommandBindings.Add(cbinding);

            #endregion
        }


    }
}

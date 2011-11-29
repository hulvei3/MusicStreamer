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

        public CommandLibrary(MainWindowViewModel parent)
        {
            _parent = parent;
            //InitializeCommands();
        }

        public void InitCommand(ICommand command, ExecutedRoutedEventHandler executed, CanExecuteRoutedEventHandler canExecute)
        {
            // TODO
            
        }


        //private void InitializeCommands()
        //{
        //    #region Custom commands

        //    // Her initialiseres de kommandoer der skal være tilgængelige som Commands til _parent.
        //    var cbinding = new CommandBinding(AddEllipse, AddEllipseExecute, AddEllipseCanExecute);
        //    _parent.CommandBindings.Add(cbinding);

        //    #endregion

        //    #region ApplicationCommands

        //    // Her initialiseres ApplicationCommands. Eftersom ApplicationCommands findes i ButtonBase.Command
        //    // overskriver vi de eksisterende, og det er ikke nødvendigt at eksponere en property.
        //    cbinding = new CommandBinding(ApplicationCommands.Undo, _undoRedoController.Undo,
        //                                  _undoRedoController.CanExecuteUndo);
        //    _parent.CommandBindings.Add(cbinding);

        //    cbinding = new CommandBinding(ApplicationCommands.Redo, _undoRedoController.Redo,
        //                                  _undoRedoController.CanExecuteRedo);
        //    _parent.CommandBindings.Add(cbinding);

        //    #endregion
        //}
    }
}

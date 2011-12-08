using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicStreamer.Interfaces;
using System.Windows;
using MusicStreamer.Exceptions;
using System.Collections.ObjectModel;
using System.Collections;

namespace MusicStreamer.CustomCommands
{
        public class UndoRedoController : PropertyAndErrorHandler
        {
            #region Singleton

            private static UndoRedoController _instance;
            public static UndoRedoController Instance
            {
                get { return _instance ?? (_instance = new UndoRedoController()); }
            }

            #endregion


            public readonly Stack<IBaseCommand> _redoStack = new Stack<IBaseCommand>();
            public readonly Stack<IBaseCommand> _undoStack = new Stack<IBaseCommand>();

            /// <summary>
            /// Metode der håndterer Redo
            /// Vi popper den seneste IAmazingCommand på stakken og udfører den. Execute kører senere i forløbet metoden PushUndoStack, så
            /// her skal vi ikke gøre det igen.
            /// </summary>
            /// <param name="sender">Afsenderobjektet</param>
            /// <param name="e">Argumenter</param>
            public void Redo(object sender, ExecutedRoutedEventArgs e)
            {
                DebugText = string.Format("Redo: {0}",e.ToString());
                var cmd = _redoStack.Pop();
                _undoStack.Push(cmd);
                cmd.Execute();
                OnPropertyChanged("RedoStack");
                OnPropertyChanged("UndoStack");
            }

            /// <summary>
            ///  Metode der afgør om Redo er muligt. Knappen disables hvis denne returnerer false
            /// </summary>
            /// <param name="sender">Afsenderobjektet</param>
            /// <param name="e">Argumenter</param>
            public void CanExecuteRedo(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = _redoStack.Count > 0;
            }

            /// <summary>
            /// Metode der håndterer Undo
            /// Vi popper den seneste AmazingCommand på stakken, udfører den omvendte handling(UnExecute), og pusher den på redostakken
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public void Undo(object sender, ExecutedRoutedEventArgs e)
            {
                DebugText = string.Format("Undo: {0}", e.ToString());
                var cmd = _undoStack.Pop();
                _redoStack.Push(cmd);
                cmd.UnExecute();
                OnPropertyChanged("RedoStack");
                OnPropertyChanged("UndoStack");
            }

            /// <summary>
            /// Metode der afgør om Undo er muligt. Knappen disables hvis denne returnerer false
            /// </summary>
            /// <param name="sender">Afsenderobjektet</param>
            /// <param name="e">Argumenter</param>
            public void CanExecuteUndo(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = _undoStack.Count > 0;
            }

            /// <summary>
            /// Metode der bruges til at pushe en IAmazingCommand på stakken udefra. Den benyttes når en kommando udføres for første gang.
            /// </summary>
            /// <param name="cmd"></param>
            public void PushUndoStack(IBaseCommand cmd)
            {
                DebugText = string.Format("Undo: *push* {0}", cmd.ToString());
                _undoStack.Push(cmd);
                _redoStack.Clear();

                OnPropertyChanged("RedoStack");
                OnPropertyChanged("UndoStack");
            }
        }
}

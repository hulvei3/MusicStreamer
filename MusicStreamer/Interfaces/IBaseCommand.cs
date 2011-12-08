using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MusicStreamer.Interfaces
{
    public interface IBaseCommand : ICommand
    {
        void Execute();
        void UnExecute();
    }
}

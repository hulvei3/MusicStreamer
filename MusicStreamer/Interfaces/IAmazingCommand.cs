using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicStreamer.CustomCommands
{
    public interface IAmazingCommand
    {
        void Execute();
        void UnExecute();
    }
}

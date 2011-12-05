using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicStreamer.Interfaces
{
    public interface IStreamerCommand
    {
        void Execute();
        void UnExecute();
    }
}

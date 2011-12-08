using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicStreamer.Interfaces;

namespace MusicStreamer.CustomCommands
{
    public class BaseCommand
    {
        public object RedoContext { get; set; }
    }
}

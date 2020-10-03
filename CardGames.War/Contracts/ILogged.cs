using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace CardGames.War.Contracts
{
    public interface ILogged
    {
        ILogger Logger { get; set; }
        void Log();
    }
}

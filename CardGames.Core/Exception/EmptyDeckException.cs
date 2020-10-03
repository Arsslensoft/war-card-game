using System;
using System.Collections.Generic;
using System.Text;

namespace CardGames.Core.Exception
{
    public class EmptyDeckException : System.Exception
    {
        public EmptyDeckException() : base("An operation has been executed on an empty deck")
        {

        }
    }
}

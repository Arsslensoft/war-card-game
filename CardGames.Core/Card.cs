using System;
using CardGames.Core.Contracts;

namespace CardGames.Core
{
    public abstract class Card : ICard
    {
        public bool IsVisible { get; set; }
        protected Card(bool isVisible = false)
        {
            IsVisible = isVisible;
        }
    }
}

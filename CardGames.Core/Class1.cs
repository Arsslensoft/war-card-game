using System;
using System.Collections.Generic;
using CardGames.Core.Helpers;

namespace CardGames.Core
{
    public interface ICard
    {
        bool IsVisible { get; set; }
    }

    public enum PlayerStatus : byte
    {
        Competing,
        Won,
        Lost
    }
    public interface IDeck<TCard> where TCard : class, ICard
    {
        IList<TCard> Cards { get; }
        TCard Draw();
        IEnumerable<TCard> Draw(int numberOfCards);
        void Put(TCard card);
        void Put(IEnumerable<TCard> cards);
    }
    public interface IPlayer<TDeck, TCard>
        where TDeck : class, IDeck<TCard>
        where TCard : class, ICard
    {
        PlayerStatus Status { get; set; }
        TDeck Deck { get; set; }
    }
    public interface ICardTray<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        IEnumerable<ICardTraySlot<TPlayer, TDeck, TCard>> PlayedCards { get; }
        void Place(TPlayer player, TCard card);
    }
    public interface ICardTraySlot<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        TPlayer Player { get; set; }
        IEnumerable<TCard> Cards { get; set; }
    }

    public interface IMove<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        void Execute(TDeck deck, ICardTray<TPlayer, TDeck, TCard> cardTray);
    }

    public interface IRoundIteration<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        ICardTray<TPlayer, TDeck, TCard> CurrentCardTray { get; }
        TPlayer Winner { get; }
        void Play();
    }
    public interface IRound<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        int Number { get; }
        IEnumerable<IRoundIteration<TPlayer, TDeck, TCard>> Iterations { get; }
        void Play();
    }

    public abstract class Card : ICard
    {
        public bool IsVisible { get; set; }
        protected Card(bool isVisible = false)
        {
            IsVisible = isVisible;
        }
    }

    public abstract class Deck<TCard> : IDeck<TCard>
        where TCard : class, ICard
    {
        private readonly List<TCard> _cards = new List<TCard>();

        public IList<TCard> Cards => _cards;
        public bool CanPick => _cards.Count > 0;


        public virtual TCard Draw()
        {
            if (_cards.Count <= 0) return null;
            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }

        public IEnumerable<TCard> Draw(int numberOfCards)
        {
            for (var i = 0; i < numberOfCards; i++)
                yield return Draw();
        }

        public virtual void Put(TCard card)
        {
            _cards.Add(card);
        }
        public virtual void Shuffle()
        {
            for (var i = _cards.Count - 1; i > 1; i--)
            {
                var randomIndex = Utils.Random.Next(i);
                var randomCard = _cards[randomIndex];
                _cards[randomIndex] = _cards[i];
                _cards[i] = randomCard;
            }
        }

        public virtual void Put(IEnumerable<TCard> cards)
        {
            _cards.AddRange(cards);
        }
    }
}

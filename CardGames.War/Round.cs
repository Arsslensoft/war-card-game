using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using CardGames.Core;
using CardGames.Core.Contracts;
using CardGames.Core.Enums;
using CardGames.Core.Exception;
using CardGames.War.StandardFiftyTwo;

namespace CardGames.War
{
    public class WarCardTraySlot : CardTraySlot<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        private readonly Stack<FiftyTwoCardGameCard> _cards = new Stack<FiftyTwoCardGameCard>();
        public override IEnumerable<FiftyTwoCardGameCard> Cards => _cards;

        public override void Put(FiftyTwoCardGameCard card)
        {
            _cards.Push(card);
        }
    }
    public class WarCardTray : CardTray<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        private readonly List<ICardTraySlot<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>> _cardTraySlots = new List<ICardTraySlot<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>>();

        public override IEnumerable<ICardTraySlot<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>>
            PlayedCards => _cardTraySlots;

        public override void Place<TCardTraySlot>(FiftyTwoCardGamePlayer player, FiftyTwoCardGameCard card)
        {
            var cardTraySlot = PlayedCards.FirstOrDefault(x => x.Player == player);
            if (cardTraySlot == null)
            {
                var newCardTraySlot = new TCardTraySlot()
                {
                    Player = player
                };
                newCardTraySlot.Put(card);
                _cardTraySlots.Add(newCardTraySlot);
            }
            else
                cardTraySlot.Put(card);
        }
    }

    public class SimplePlayerMovementController : MoveController<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        public override bool Execute(FiftyTwoCardGamePlayer player, ICardTray<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard> cardTray)
        {
            var card = player.Deck.Draw();
            if (card == null) // deck is empty the player has lost
                return false;
            cardTray.Place<WarCardTraySlot>(player, card);

            return true;
        }
    }
    public class WarPlayerMovementController : MoveController<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        public override bool Execute(FiftyTwoCardGamePlayer player, ICardTray<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard> cardTray)
        {
            var isVisible = false;
            foreach (var fiftyTwoCardGameCard in player.Deck.Draw(2)) // the player must put his card on the tray if he has one
            {

                if (fiftyTwoCardGameCard == null) // deck is empty the player has lost
                    return false;
                if (!isVisible) // set the first card as invisible
                {
                    fiftyTwoCardGameCard.IsVisible = false;
                    isVisible = true;

                }
                cardTray.Place<WarCardTraySlot>(player, fiftyTwoCardGameCard);
            }
            return true;
        }
    }
    public class WarCardRoundIteration : RoundIteration<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        public bool HasConflict
        {
            get
            {
                var conflictingPlayersByTopCard = CurrentCardTray.PlayedCards
                    .Select(x => new { x.Player, Card = x.Cards.FirstOrDefault() })
                    .GroupBy(x => x.Card.Face)
                    .OrderByDescending(x => x.Key)
                    .FirstOrDefault();
                return conflictingPlayersByTopCard != null && conflictingPlayersByTopCard.Count() > 1;
            }
        }
        public IEnumerable<FiftyTwoCardGamePlayer> PlayersInConflict
        {
            get
            {
                var conflictingPlayersByTopCard = CurrentCardTray.PlayedCards
                    .Select(x => new { x.Player, Card = x.Cards.FirstOrDefault() })
                    .GroupBy(x => x.Card.Face)
                    .OrderByDescending(x => x.Key)
                    .FirstOrDefault();
                if (conflictingPlayersByTopCard == null || conflictingPlayersByTopCard.Count() <= 1) yield break;

                foreach (var playerCards in conflictingPlayersByTopCard)
                    yield return playerCards.Player;
            }
        }
        public override FiftyTwoCardGamePlayer Winner => CurrentCardTray
            .PlayedCards
            .Aggregate((l, r) => l?.Cards?.FirstOrDefault()?.CompareTo(r.Cards.FirstOrDefault()) == 1 ? l : r).Player;
        public override void Play()
        {
            foreach (var player in Players)
                if (!MoveController.Execute(player, CurrentCardTray))
                    player.Status = PlayerStatus.Lost;
        }
    }

    public class WarCardRound : Round<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        public override void Play()
        {
            // create the first iteration with a simple movement controller
            var currentIteration = CreateIteration<WarCardRoundIteration, SimplePlayerMovementController, WarCardTray>();
            currentIteration.Play();
            // detect conflict and enter war mode if possible
            while (currentIteration.HasConflict)
            {
                var playersInConflict = currentIteration.PlayersInConflict;
                currentIteration = CreateIteration<WarCardRoundIteration, WarPlayerMovementController, WarCardTray>(playersInConflict);
                currentIteration.Play();
            }
            Winner?.Deck.Put(AllPlayedCards);
            // Eliminate players with an empty deck
            foreach (var player in Players)
                if (!player.Deck.CanPick)
                    player.Status = PlayerStatus.Lost;
        }
    }
    public class WarCardGame : Game<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        public WarCardGame(IEnumerable<FiftyTwoCardGamePlayer> players, FiftyTwoCardGameDeck initialDeck)
        {
            Players = players;
            InitialDeck = initialDeck;
        }
        public override FiftyTwoCardGamePlayer Winner
        {
            get
            {
                var candidates = Players.Where(player => player.Status == PlayerStatus.Competing).ToList();
                return candidates.Count == 1 ? candidates.FirstOrDefault() : null;
            }
        }
        public override void Play()
        {
            var roundNumber = 0;
            var candidates = Players.Where(player => player.Status == PlayerStatus.Competing).ToList();
            // while there are some players keep the game going
            while (candidates.Count > 1)
            {
                // create a new round and play
                var round = CreateRound<WarCardRound>(candidates, ++roundNumber);
                round.Play();
                // update candidates list
                candidates = Players.Where(player => player.Status == PlayerStatus.Competing).ToList();
            }
        }
    }
}

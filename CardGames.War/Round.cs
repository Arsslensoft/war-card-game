using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGames.Core;
using CardGames.Core.Contracts;
using CardGames.Core.Enums;
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
        public override void Execute(FiftyTwoCardGameDeck deck, ICardTray<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard> cardTray)
        {
            throw new NotImplementedException(); //TODO:Implement the move
        }
    }
    public class WarPlayerMovementController : MoveController<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        public override void Execute(FiftyTwoCardGameDeck deck, ICardTray<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard> cardTray)
        {
            throw new NotImplementedException(); //TODO:Implement the move
        }
    }
    public class WarCardRoundIteration : RoundIteration<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        public bool HasConflict
        {
            get
            {
                var groupedPlayersByCard = CurrentCardTray.PlayedCards
                    .Select(x => new { x.Player, Card = x.Cards.FirstOrDefault() })
                    .GroupBy(x => x.Card.Face);

                return groupedPlayersByCard.Count() != CurrentCardTray.PlayedCards.Count();
            }
        }
        public IEnumerable<FiftyTwoCardGamePlayer> ConflictingPlayers
        {
            get
            {
                var groupedPlayersByCard = CurrentCardTray.PlayedCards
                    .Select(x => new { x.Player, Card = x.Cards.FirstOrDefault() })
                    .GroupBy(x => x.Card.Face);

                foreach (var playerCards in groupedPlayersByCard)
                {
                    if (playerCards.Count() <= 1) continue;
                    foreach (var playerCard in playerCards)
                        yield return playerCard.Player;
                }
            }
        }
        public override FiftyTwoCardGamePlayer Winner => throw new NotImplementedException();
        public override void Play()
        {
            throw new NotImplementedException();
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
                currentIteration = CreateIteration<WarCardRoundIteration, WarPlayerMovementController, WarCardTray>();
                currentIteration.Play();
            }
            Winner?.Deck.Put(AllPlayedCards);
        }
    }
    public class WarCardGame : Game<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
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

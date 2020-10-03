using CardGames.Core.Contracts;
using CardGames.Core.Enums;

namespace CardGames.Core
{
    public abstract class Player<TDeck, TCard> : IPlayer<TDeck, TCard>
        where TDeck : class, IDeck<TCard>
        where TCard : class, ICard
    {
        /// <inheritdoc cref="IPlayer{TDeck,TCard}" />
        public string Name { get; set; }

        /// <inheritdoc cref="IPlayer{TDeck,TCard}" />
        public PlayerStatus Status { get; set; }

        /// <inheritdoc cref="IPlayer{TDeck,TCard}" />
        public TDeck Deck { get; set; }

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return $"[{Name}, Deck={Deck}, Status={Status}]";
        }
    }
}
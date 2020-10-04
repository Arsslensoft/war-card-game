# Understanding the classes
In this section we will discuss the purpose of each class and define the ways to extend it.

# Base Classes
The project is composed of a set of classes that represent its foundation, they are built to be extensible and can be easily substitutable. These classes include:
* ``Card``: This class is the representation of the card you are willing to use, for example if you are using UNO, you must define a class that represents it and which inherits from the ``Card`` class.
* ``Deck``: This class represents the deck of card of the given game.
* ``Player``: This class represents the player information in a given game.
* ``CardTraySlot``: This class represents the cards placed in the tray that are specific to a player un a given game.
* ``CardTray``: This class represents card tray where the players place their cards during a round.
* ``Round``: This class represents the logic of a round, this class will evaluate conditions, create iterations and determines if a player has won the round or not.
* ``RoundIteration``: This class represents the logic of a round iteration, this class will evaluate conditions, execute moves and orchestrate the card placement in the tray. This class contributes to winner determination at the iteration level.
* ``MoveController``: This class represents the move logic of a player, this class will help you define the moves logic which you can call if the ``Round`` or ``RoundIteration``.
* ``Game``: This class represents the game information and logic.
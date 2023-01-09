# Othello-in-CSharp
This project illustrates the Othello game - a board game invented in 1880.
It is constructed in an OOP architecture, with a seperation between UI and logic.

The game can be played in a 8x8 board or 6x6 board.
Each player has its color - Black/White.
At each turn, the player aims to use his coin to block the second player's sequence of coins that is blocked on the other side by another coin of his. By that, the sequence of coins turns color in favor of the player who's currently playing.
The winner is the player with the most coins on board (when the board is full).

# What does the project support?
1. The player can choose whether he wants to play against another player or against the computer. The computer's moves are based on Random.Next.
2. The player can choose to play in a 8x8 board or 6x6.
3. If a player chooses an illegal move, he will be prompted to enter a new move with a relevant message.
4. The game prompts the user whenever he enters an invalid input (Syntax etc.)
5. At each turn, the player can enter 'Q' to quit the game.

And more!

# ASBG

ASBG is an online 2 player board game modelled using ideas from the game of chess and memoir44. The goal of the game is to capture
the opponent's general by slowly advancing game pieces in strategic locations.

## Motivation 

We have always wanted to try making our own version of a strategic game. It is an idea that we found interesting and could be an 
additional feature to the existing game of chess. We hope to be able to put all the coding practices we have done into developing 
something practical and achievement-worthy.

## Target audience

We create this game for users who love brain-stimulating challenges and people who seek entertainment through multiplayer games.
Our goal would be to successfully attract a sustainable number of users so each player can play against a diverse range of other
users through online match-making.

## Game features

1. Main menu for players to start game and exit. Both players will be using the same console (to be upgraded to online multiplayer
   if time and complexity permits).

2. The game board is a 9x9 board with red pieces on one side and blue pieces on the other side, just like the game of chess.

3. Pieces include archer, scout, mage, infantry, cavalry and general. 

4. The game board is designed such that each player will have 2 rows of his own pieces, with the front row filled with infantry and
   and the back row containing scout, mage, archer, cavalry, general, cavalry, archer, mage and scout in that order.

5. Each piece is assigned 4 attributes, which are health, attack power, attack range and movement range.

6. Specific tiles on the board are 'special' tiles, which give certain pieces buffs that increase specific attributes.

7. One turn consists of 2 actions, which are to either attack or move. Moving allows selected piece to change position. Attack allows
   piece to launch an attack on a target piece.   

## Rules

1. The goal of the game is to successfully capture the opposing general. The game ends when either the player's or opponent's general's    health reaches zero, or when a player surrenders.

2. At the start of the game, each player will choose between 3 types of generals, shining knight, dragon knight or armored titan.   

3. Each player will take turns making their moves. A turn consists of 2 actions, which are to attack or move. In one turn, a player
   can select any piece and must execute exactly 2 moves. 

4. Each piece can only move over a pre-assigned number of steps or attack over a pre-assigned area of cells. 

5. No piece is able to take over the position of another piece unless that piece dies or moves away.

6. Attributes
  * Hp
      - Life of pieces
      - Piece dies when Hp reaches 0
  * Movement speed
      - Number of cells a piece is able to move to
      - 1 (only adjacent 4 cells)
      - 2 (2 cells away, non-diagonal)
      - 3 (3 cells away, non-diagonal)
      - 4 (4 cells away, non-diagonal)
  * Attack
      - Attack power of the unit, used to determine outcome of an attack
  * Attack range
      - Number of cells away a piece is able to attack on
      - 1 (only adjacent 4 cells)
      - 2 (2 cells away, non-diagonal)
      - 3 (3 cells away, non-diagonal)
 
7. Movement 
    - Pieces can only move based on the movement speed attribute it has. Any number of steps less than or equal to the movement
      speed is allowed (minimum of 1)
    - Potential cells would be highlighted on the board

8. Attack     
In one turn, a player can use one move to select a piece and launch an attack on target piece. The outcome of the attack is
decided by dice rolls, one for the attacker and one for the defender. The dice roll of the attacker will be added to his attack
power and the dice roll of the defender will be added to the defender's attack power. If the sum for the attacker is greater than
the sum for the defender, the difference is taken and the defender's Hp is reduced by that amount. If the sum for the attacker is
less than or equal to the sum for the defender, no attack occurs and nothing happens.

9. Pieces (attributes are tentative)
  * Scouts
     * Fast and deadly unit. Hp 4 Movement speed 3 Attack 3 Attack range 1
  * Archers
     * Long-ranged unit. Hp 5 Movement speed 1 Attack 2 Attack range 3
  * Mage
     * Support unit. Hp 6 Movement speed 1 Attack 2 Attack range 2
     * Can 'attack' own unit, which increases the Hp of the target by 2 points
  * Cavalry
     * Balanced unit. Hp 8 Movement speed 2 Attack 2 Attack range 1
  * Infantry
     * normal unit. Hp 7 Movement speed 1 Attack 2 Attack range 1
  * Generals
      * boss unit 
      * Shining Knight Hp 10 Movement speed 1 Attack 3 Attack range 2
      * Dragon Knight Hp 8 Movement speed 2 Attack 4 Attack range 1
      * Armored Titan Hp 12 Movement speed 1 Attack 3 Attack range 1
 
10. Buff cells (triggered only on this cell)
   * Hill
      * Scouts - Movement speed +1
      * Archers - Attack +1
      * Infantry - Attack +1
      * Cavalry - Attack +1
      * Mage - Attack range +1
      * Shining Knight - Attack +1
      * Dragon Knight - Attack range +1
      * Armored Titan - Movement speed +1
   * Barracks 
      * Infantry - Hp +1 Attack +1 

## Execution plan

May - 1st week of June 
- Go through online tutorials on unity and c#
- Brainstorming of game pieces
- Discussing and testing of game mechanics

Last week of May
- Milestone 1 preparation
- Develop main frame and board for game
- Develop pieces to be shown on gameboard
- Input basic movement for pieces

June
- Ensure game in unity is turn-based and pieces can move to their designated spots
- Add attack option for game pieces
- Assign Health to be displayed for game pieces
- Code in special effects for special terrain(hill, barracks)
- Milestone 2 preparation

June to July
- Testing game mechanics on game and ensure gameplay is balanced

By 1st week of July
- Create working game board prototype

July 
- Adding in game menu interface for for start game and game guide
- Ensure pieces can make decisions to move or kill 
- Adding animation for movement and attack

Last week of July
- Research on photon unity networking
- Milestone 3 preparation

By 1st week of August
- Complete game prototype with game menu and game guide

August
- Implement game in online multiplayer mode 
- Upgrade graphics if time permits

By end August
- Finalize game and ensure 2 players can play on 2 different laptops
- Splashdown preparation

# Maze Runner
This is a maze exploration game where players navigate through randomly generated mazes while avoiding deadly zones and achieving various objectives. Players must utilize shields to survive hazards and reach designated goal areas to progress to the next level.

__Features:__

- Maze Generation: Each level features a randomly generated maze with walls and pathways, ensuring unique gameplay experiences.
- Deadly Zones: The game includes deadly zones represented by red areas that players must avoid unless they activate a protective shield.
- Shield Mechanism: Players can activate a shield by holding a button on the UI, allowing safe passage through deadly zones for a limited time.
- Player Respawn: Upon collision with deadly zones while the shield is inactive, players experience a visual death animation and respawn at the starting position without regenerating the maze.
- Level Completion: Upon reaching designated green zones, players trigger a celebration animation with confetti and transition to a new randomly generated maze.

_It is developed as a Unity project using C#._

## Prerequisites

To run the application, it is necessary to install [Unity 2022](https://unity.com/releases/editor/archive).

## Screenshots

![Game example](https://raw.github.com/serhii721/labyrinth-game/screenshots/Screenshots/1.png "Game example")
![Another game example](https://raw.github.com/serhii721/labyrinth-game/screenshots/Screenshots/2.png "Another game example")
![Project window](https://raw.github.com/serhii721/labyrinth-game/screenshots/Screenshots/3.png "Project window")

## Configuration
In the settings, a user can:

- Change maze dimensions (width and height).
- Adjust the number of deadly zones generated in each maze.
- Set the time duration for the shield activation.

## Development Notes

_Unity 2022.3.42f1 and Microsoft Visual Studio 2017 (15.9.59) were used for development._

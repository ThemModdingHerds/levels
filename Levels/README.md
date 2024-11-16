# ThemModdingHerds.Worlds

parser/writer of Them's Fightin' Herds `worlds.ini` files in `levels.gfs`

## Usage

```c#
using ThemModdingHerds.Levels;

LevelData levelData = LevelData.Read(folderWithLvlNextToIt);
Level lvl = levelData.Lvl; // contains data from .lvl file
```

## Syntax

See [here][syntax-file] for more info

[syntax-file]: ./SYNTAX.md

# ThemModdingHerds.Worlds

parser/writer of Them's Fightin' Herds `worlds.ini` files in `levels.gfs`

## Usage

```c#
using ThemModdingHerds.Levels;

LevelData levelData = LevelData.Read(folderWithLvlNextToIt);
Level lvl = levelData.Lvl; // contains data from .lvl file
```

## Level Packs

The class `LevelPack` can be used to create level packs (for example the `temp/levels` in `levels.gfs` counts as a level pack). You can then combine multiple level packs into one. It should have the following structure:

- the main folder (let's call it the root folder)
- the `textures` folder inside the root folder
- the `worlds.ini` file inside the root folder
- all the `.lvl` files and their data folder inside the root folder

Example:

```sh
/mylevelpack/ # this is the root folder
/mylevelpack/worlds.ini # this file should exists
/mylevelpack/mycustomstage1.lvl # a custom level
/mylevelpack/mycustomstage/ # the folder is not a requirement but recommended
/mylevelpack/textures/mycustomstagetexture.dds # a texture that mycustomstage references
```

## Syntax

See [here][syntax-file] for more info

[syntax-file]: ./SYNTAX.md

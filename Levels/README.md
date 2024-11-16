# ThemModdingHerds.Worlds

parser/writer of Them's Fightin' Herds `worlds.ini` files in `levels.gfs`

## Usage

```c#
using ThemModdingHerds.Levels;

// parse string to get entries
Worlds worlds = Worlds.Parse(content);
// you can even read it from a file
Worlds worlds = Worlds.Read("path/to/worlds.ini");
// Worlds is extending List<WorldEntry>
WorldEntry entry = worlds[0];

// write all entries into a valid worlds.ini structure
string filecontent = worlds.ToString();
```

## Syntax

See [here][syntax-file] for more info

[syntax-file]: ./SYNTAX.md

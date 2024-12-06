# Syntax

## `worlds.ini`

### Entry (`worlds.ini`)

`<type> <name> [prop_key] prop_value [prop_key2] prop_value2 ...`

### Special Properties

- `desc` = text below stage selection, value is the text
- `img` = the stage cover image, value is path to stage cover relative to OtterUI folder (`<game-folder>/dev/temp/UI/*/Output/`)
  - if you're on Windows (`Win`) it might have `*.dds` files, but the `[img]` property value REQUIRES to be `*.png`
- `dlc` = marks stage as DLC stage, value is DLC name (?)
- `tags` = gives stage special tag(s), value is tags

### Notes

- `#` are comments
- `[desc]` properties are required (for stage select)
- `[img]`,`[dlc]` and `[tags]` properties are optional
- `\n` and `\r` (if on windows) are ignored

## `stages.ini`

### Entry (`stages.ini`)

`<type> <name> <stage_select_picture_name> prop_key>prop_value prop_key2>prop_value...`

## `*.lvl`

### Properties

|Name|Parameters|Notes|
|----|----------|-----|
|`StageSize:`|\<width> \<height>|Size of level (Boundaries)|
|`BottomClearance:`|\<y>|Bottom of the Level|
|`Start1:`|\<x>|Start of Player 1 (left)|
|`Start2:`|\<x>|Start of Player 2 (right)|
|`ShadowDir:`|`D`|Direction of the shadow casts|
|`Light: Amb`|\<red> \<green> \<blue>|Ambient Light of the level|
|`Light: Pt`|\<red> \<green> \<blue> \<x1> \<y1> \<x2> \<y2>|Light Point from `(x1,y1)` to `(x2,y2)`|
|`Light: Dir`|\<red> \<green> \<blue> \<x> \<y> \<z>|Light Direction of the level|
|`SUN`|\<???> \<???> \<???> \<???> \<???> \<???> \<???> \<???> \<???>|Something Sun of the level|
|`PROJ`|\<???> \<???> \<???> \<???> \<???>|Projection? Like the matrix?|
|`SHADOWDIST`|\<distance>|Length of the shadow casts|
|`Reverb`|[\<type>](#reverb-types) \<wetness>|Reverb type and strength of the level|
|`2D`|\<path>|Background Image when set Graphics to 2D|
|`3D`|\<tilt-rate> \<tilt-height1> \<tilt-height2>|Camera tilting up when between `tilt-height1` and `tilt-height2`, interpolated with rate `tilt-rate`|
|`CAMERA`|\<field-of-view> \<z-near> \<z-far>|Camera setttings of the level|

### Reverb Types

- OFF (Default)
- GENERIC
- PADDEDCELL
- ROOM
- BATHROOM
- LIVINGROOM
- STONEROOM
- AUDITORIUM
- CONCERTHALL
- CAVE
- ARENA
- HANGAR
- CARPETTEDHALLWAY
- HALLWAY
- STONECORRIDOR
- ALLEY
- FOREST
- CITY
- MOUNTAINS
- QUARRY
- PLAIN
- PARKINGLOT
- SEWERPIPE
- UNDERWATER

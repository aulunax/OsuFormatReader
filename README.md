# OsuFormatReader

For reading .osu beatmap files into c# objects

### Types descriptions:

Object types mostly adhere to
[this](https://github.com/ppy/osu-wiki/blob/fad1a0b49c66ddf75aa00ebe701f8d607de53272/wiki/Client/File_formats/osu_(file_format)/en.md)
page in the osu-wiki, with few additional enums for better readability,
and changing decimal to double for few properties,
to better represent unusual values possible for them in the actual game.

### Current issues:

- Lack of storyboard reading support (they are simply ignored)
- Mostly tested on std maps, other game modes might not be read as expected

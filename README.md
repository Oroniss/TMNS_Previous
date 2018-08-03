# TMNS

Building off the beginnings of the Engine repository. Largely going to mimic the original Python version, but it incorporates
some of the lessons learned from other projects done in the mean time.

# Major Pieces

Levels - the basic building block of the game - too large class but keeps everything together.

Actors - split into Monster and Player.

Furnishings - separated into various other options as well.

Items and many subclasses.

Effects - anything that modifies an entity.

Events - everything that happens generates events - interacts with achievements and quests primarily.

# Resources

Uses a db for storing data - will eventually put the level templates in there too.
A lot of smaller classes/functions as well.

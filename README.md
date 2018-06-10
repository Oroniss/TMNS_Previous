# RLEngine

Trying a different approach to creating the engine - not using an ECS - just going with a few simple entity classes and a "level" option.

# Major Pieces

Levels - the basic building block of the game - too large class but keeps everything together.

Actors - split into NPC and Player.
Furnishings - can be split if required.

Items can be added if required.

Effects - anything that modifies an entity.

Events - everything that happens generates events - interacts with achievements and quests primarily.

# Resources

Allows for a db or text files to be used for storing data.
A lot of smaller classes/functions as well.

# tile

This code works with Unity3d 2017's tilemapping features to create a tileset that automatically chooses the correct tile.
It is optimized for a set of 20 platformer map tiles but can also be used for a top-down game or be updated to include up to 256 unique tiles.

Simply select all of your tiles, ensuring they are in the proper order, and drag them onto the "Sprite List" field in the inspector. This will only work if the size of the array is set to 0, otherwise the sprites will have to be manually added.

If you want to change the tile order, you will have to calculate the bitmasking value for each sprite. Imagine the sprite (X) and its eight adjacent tiles (a-h), like so:

a  b  c  
d  X  e  
f  g  h  

The bitmasking value is a binary number based on the adjacent sprites. If there is a tile in that location, it is a 1; if there is no tile in that location, it is a 0. Tile h is 2^0 and tile a is 2^8, so the final value will be in the order abcd efgh.

Once you have the binary bitmasking value, convert it to decimal. You must update the SpriteDict file with a key, value pair for the tile. The unique key should be the bitmasking value (a number from 0-255) and the value should be the index of the sprite in the array.

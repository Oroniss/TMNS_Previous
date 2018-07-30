// Tidied for version 0.3.

using System;
using System.Collections.Generic;

namespace RLEngine.Levels
{
	[Serializable]
	public class LevelSaveSummary
	{
		public LevelId LevelId;
		public string LevelName;
		public int Height;
		public int Width;

		public int[] Tiles;
		public bool[] Revealed;
		public Dictionary<int, Entities.MapTiles.TileType> TileTypes = new Dictionary<int, Entities.MapTiles.TileType>();

		public Dictionary<int, int> Furnishings = new Dictionary<int, int>();
		public Dictionary<int, int> Actors = new Dictionary<int, int>();
	}
}

using System;
using System.Collections.Generic;

namespace RLEngine.Levels
{
	[Serializable]
	public class LevelSaveSummary
	{
		public LevelId levelId;
		public string levelName;
		public int height;
		public int width;

		public int[] tiles;
		public bool[] revealed;
		public Dictionary<int, Entities.MapTiles.TileType> tileTypes = new Dictionary<int, Entities.MapTiles.TileType>();

		public Dictionary<int, int> furnishings = new Dictionary<int, int>();
		public Dictionary<int, int> actors = new Dictionary<int, int>();
	}
}

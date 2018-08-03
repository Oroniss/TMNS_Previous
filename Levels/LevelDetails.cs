// Tidied for version 0.3.

using System.Collections.Generic;
using System;

namespace TMNS.Levels
{
	[Serializable]
	public class LevelDetails
	{
		public readonly string LevelName;
		public readonly LevelId LevelId;
		public int MapWidth;
		public int MapHeight;
		public Dictionary<int, Entities.MapTiles.TileType> TileDictionary;
		public int[] MapGrid;

		public List<string[]> Furnishings;

		public LevelDetails(string levelName, LevelId levelId)
		{
			LevelName = levelName;
			LevelId = levelId;
			TileDictionary = new Dictionary<int, Entities.MapTiles.TileType>();
			Furnishings = new List<string[]>();
		}
	}
}

using System.Collections.Generic;
using System;

namespace RLEngine.Levels
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

		public LevelDetails(string levelName, LevelId levelId)
		{
			LevelName = levelName;
			LevelId = levelId;
			TileDictionary = new Dictionary<int, Entities.MapTiles.TileType>();
		}
	}
}

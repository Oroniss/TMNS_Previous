using System.Collections.Generic;

namespace RLEngine.Levels.LevelTemplate
{
	public class LevelTemplate
	{
		public int MapWidth;
		public int MapHeight;
		public Dictionary<int, RLEngine.Entities.MapTiles.TileType> TileDictionary;
		public int[] MapGrid;

		public LevelTemplate()
		{
		}
	}
}

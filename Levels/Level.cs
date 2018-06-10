using System.Collections.Generic;
using System;
using RLEngine.Entities.MapTiles;

namespace RLEngine.Levels
{
	[Serializable]
	public class Level
	{
		readonly string _levelName;
		readonly int _mapWidth;
		readonly int _mapHeight;
		readonly int[] _tileGrid;
		readonly Dictionary<int, MapTileDetails> _tileInformation;
		readonly bool[] _revealed;

		readonly SortedDictionary<int, int> _furnishings;
		readonly SortedDictionary<int, int> _actors;

		public Level(string levelName)
		{
			_furnishings = new SortedDictionary<int, int>();
			_actors = new SortedDictionary<int, int>();
		}

		// The basic public stuff
		public string LevelName
		{
			get { return _levelName; }
		}

		public int MapWidth
		{
			get { return _mapWidth; }
		}

		public int MapHeight
		{
			get { return _mapHeight; }
		}

		// Public utility functions.
		public bool IsValidMapCoord(int x, int y)
		{
			return 0 <= x && x < _mapWidth && 0 <= y && y < _mapHeight;
		}

		// Graphics functions
		public bool IsRevealed(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return _revealed[index];
		}

		public void RevealTile(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			_revealed[index] = true;
		}

		public string GetBGColor(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return _tileInformation[_tileGrid[index]].BackgroundColor;
		}

		public string GetFogColor(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return _tileInformation[_tileGrid[index]].FogColor;
		}


		// Private helper functions
		int ConvertXYToInt(int x, int y)
		{
			return y * _mapWidth + x;
		}
	}
}

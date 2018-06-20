using System.Collections.Generic;
using System;
using RLEngine.Entities.MapTiles;
using RLEngine.Entities.Actors;

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

		public Level(LevelId levelId)
		{
			_furnishings = new SortedDictionary<int, int>();
			_actors = new SortedDictionary<int, int>();

			var levelTemplate = LevelDatabase.LevelDatabase.GetLevelTemplate(levelId);

			// Basic details
			_levelName = levelTemplate.LevelName;
			_mapWidth = levelTemplate.MapWidth;
			_mapHeight = levelTemplate.MapHeight;
			_tileGrid = levelTemplate.MapGrid;
			_revealed = new bool[_mapWidth * _mapHeight];

			_tileInformation = new Dictionary<int, MapTileDetails>();
			foreach (KeyValuePair<int, TileType> tile in levelTemplate.TileDictionary)
				_tileInformation[tile.Key] = MapTileDetails.GetTileDetails(tile.Value);
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

		public bool HasTrait(Entities.Trait trait, int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return HasTrait(trait, index);
		}

		bool HasTrait(Entities.Trait trait, int index)
		{
			// TODO: Add in furnishing and other entity types as appropriate.
			return _tileInformation[_tileGrid[index]].HasTrait(trait) ||
					(_actors.ContainsKey(index) && Actor.GetActor(_actors[index]).HasTrait(trait));
		}


		// Graphics functions
		public bool IsRevealed(int x, int y)
		{
			if (IsValidMapCoord(x, y))
			{
				var index = ConvertXYToInt(x, y);
				return _revealed[index];
			}
			ErrorLogger.AddDebugText(string.Format("Checked revealed on invalid tile: {0}, {1}", x, y));
			return false;
		}

		public void RevealTile(int x, int y)
		{
			if (IsValidMapCoord(x, y))
			{
				var index = ConvertXYToInt(x, y);
				_revealed[index] = true;
			}
			else
				ErrorLogger.AddDebugText(string.Format("Tried to reveal an invalid tile: {0}, {1}", x, y));
		}

		public string GetBGColor(int x, int y)
		{
			if (IsValidMapCoord(x, y))
			{
				var index = ConvertXYToInt(x, y);
				return _tileInformation[_tileGrid[index]].BackgroundColor;
			}
			ErrorLogger.AddDebugText(string.Format("Attempted to get bgcolor on invalid tile: {0}, {1}", x, y));
			return "Black";
		}

		public string GetFogColor(int x, int y)
		{
			if (IsValidMapCoord(x, y))
			{
				var index = ConvertXYToInt(x, y);
				return _tileInformation[_tileGrid[index]].FogColor;
			}
			ErrorLogger.AddDebugText(string.Format("Attempted to get fogcolor on invalid tile: {0}, {1}", x, y));
			return "Black";
		}

		public bool HasDrawingEntity(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return HasDrawingEntity(index);
		}

		bool HasDrawingEntity(int index)
		{
			// TODO: Add others in here as needed.
			return _actors.ContainsKey(index) || _furnishings.ContainsKey(index);
		}

		public Entities.Entity GetDrawingEntity(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return GetDrawingEntity(index);
		}

		Entities.Entity GetDrawingEntity(int index)
		{
			// TODO: keep these up to date
			if (_actors.ContainsKey(index))
				return Actor.GetActor(_actors[index]);
			if (_furnishings.ContainsKey(index))
				// TODO: Add this is later.
				return null;
			// TODO: Add error message here.
			return null;
		}

		// Actor functions
		public bool HasActor(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return HasActor(index);
		}

		bool HasActor(int index)
		{
			return _actors.ContainsKey(index);
		}

		public Actor GetActor(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return GetActor(index);
		}

		Actor GetActor(int index)
		{
			if (_actors.ContainsKey(index))
				return Actor.GetActor(_actors[index]);
			return null;
		}

		public void AddActor(Actor actor)
		{
			AddActor(actor.ActorId, actor.XLoc, actor.YLoc);
		}

		public void AddActor(int actorId, int xLoc, int yLoc)
		{
			var index = ConvertXYToInt(xLoc, yLoc);
			AddActor(actorId, index);
		}

		void AddActor(int actorId, int index)
		{
			if (_actors.ContainsKey(index))
			{
				// TODO: Add error text
			}
			_actors[index] = actorId;
		}

		public void RemoveActor(Actor actor)
		{
			RemoveActor(actor.ActorId, actor.XLoc, actor.YLoc);
		}

		public void RemoveActor(int actorId, int xLoc, int yLoc)
		{
			var index = ConvertXYToInt(xLoc, yLoc);
			RemoveActor(actorId, index);
		}

		void RemoveActor(int actorId, int index)
		{
			if (_actors.ContainsKey(index))
			{
				if (_actors[index] == actorId)
					_actors.Remove(index);
				else
					// TODO: Add error text
					return;
			}
			else
			{
				// TODO: Add error text
			}
		}


		// Movement and pathability functions.
		public bool IsPassible(Actor actor, int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return IsPassible(actor, index);
		}

		bool IsPassible(Actor actor, int index)
		{
			// TODO: Add in additional movement functions if required.
			return !HasTrait(Entities.Trait.BlockMove, index);
		}


		// Private helper functions
		int ConvertXYToInt(int x, int y)
		{
			return y * _mapWidth + x;
		}
	}
}

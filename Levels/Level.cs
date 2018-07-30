// Tidied for version 0.3.

using System.Collections.Generic;
using System;
using RLEngine.Entities.MapTiles;
using RLEngine.Entities.Actors;
using RLEngine.Entities.Furnishings;
using RLEngine.Resources.Geometry;

namespace RLEngine.Levels
{
	[Serializable]
	public class Level
	{
		static readonly int[,] _octantTranslate = {
			{1, 0, 0, -1, -1, 0, 0, 1},
			{0, 1, -1, 0, 0, -1, 1, 0},
			{0, 1, 1, 0, 0, -1, -1, 0},
			{1, 0, 0, 1, -1, 0, 0, -1}
		};

		const int FURNISHINGSTARTINGINDEX = 3;

		readonly LevelId _levelId;
		readonly string _levelName;
		readonly int _mapWidth;
		readonly int _mapHeight;
		readonly int[] _tileGrid;
		readonly Dictionary<int, MapTileDetails> _tileInformation;
		readonly bool[] _revealed;

		readonly SortedDictionary<int, Furnishing> _furnishings;
		readonly SortedDictionary<int, Actor> _actors;

		List<XYCoordinateStruct> _visibleTiles;

		public Level(LevelId levelId)
		{
			_furnishings = new SortedDictionary<int, Furnishing>();
			_actors = new SortedDictionary<int, Actor>();

			var levelTemplate = LevelDatabase.LevelDatabase.GetLevelTemplate(levelId);

			// Basic details
			_levelId = levelId;
			_levelName = levelTemplate.LevelName;
			_mapWidth = levelTemplate.MapWidth;
			_mapHeight = levelTemplate.MapHeight;
			_tileGrid = levelTemplate.MapGrid;
			_revealed = new bool[_mapWidth * _mapHeight];

			_visibleTiles = new List<XYCoordinateStruct>();

			_tileInformation = new Dictionary<int, MapTileDetails>();
			foreach (KeyValuePair<int, TileType> tile in levelTemplate.TileDictionary)
				_tileInformation[tile.Key] = Entities.EntityFactory.CreateMapTile(tile.Value);

			// Furnishings
			foreach (string[] furnishing in levelTemplate.Furnishings)
			{
				if (furnishing.Length < FURNISHINGSTARTINGINDEX)
				{
					ErrorLogger.AddDebugText(string.Format("Misformed line in furnishing read: {0}", furnishing));
					continue;
				}

				var furnishingName = furnishing[0];
				var xLoc = int.Parse(furnishing[1]);
				var yLoc = int.Parse(furnishing[2]);
				var otherParameters = ParseOtherEntityParameters(furnishing, FURNISHINGSTARTINGINDEX);

				var newFurnishing = Entities.EntityFactory.CreateFurnishing(furnishingName, xLoc, yLoc, otherParameters);
				AddFurnishing(newFurnishing);
			}
		}

		public Level(LevelSaveSummary summary)
		{
			_levelId = summary.LevelId;
			_levelName = summary.LevelName;

			_mapWidth = summary.Width;
			_mapHeight = summary.Height;

			_tileInformation = new Dictionary<int, MapTileDetails>();
			_tileGrid = summary.Tiles;
			foreach (KeyValuePair<int, TileType> tile in summary.TileTypes)
				_tileInformation[tile.Key] = Entities.EntityFactory.CreateMapTile(tile.Value);
			_revealed = summary.Revealed;

			_furnishings = new SortedDictionary<int, Furnishing>();
			foreach (KeyValuePair<int, int> furnishing in summary.Furnishings)
				_furnishings[furnishing.Key] = Furnishing.GetFurnishing(furnishing.Value);

			_actors = new SortedDictionary<int, Actor>();
			foreach (KeyValuePair<int, int> actor in summary.Actors)
				_actors[actor.Key] = Actor.GetActor(actor.Value);
		}

		// Pack up a level that's no longer needed.
		public void Dispose()
		{
			foreach (Furnishing furnishing in _furnishings.Values)
				furnishing.Dispose();
			foreach (Actor actor in _actors.Values)
				actor.Dispose();
			// TODO: Keep this up to date if need be.
		}


		// The basic public stuff
		public LevelId LevelId
		{
			get { return _levelId; }
		}

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
				(_actors.ContainsKey(index) && _actors[index].HasTrait(trait)) ||
				(_furnishings.ContainsKey(index) && _furnishings[index].HasTrait(trait));
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
				return _actors[index];
			if (_furnishings.ContainsKey(index))
				return _furnishings[index];
			// TODO: Add error message here.
			return null;
		}

		// LOS and FOV
		public bool BlockLOS(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return BlockLOS(index);
		}

		bool BlockLOS(int index)
		{
			return HasTrait(Entities.Trait.BlockLOS, index);
		}

		public List<XYCoordinateStruct> VisibleTiles
		{
			get { return _visibleTiles; }
			set { _visibleTiles = value; }
		}

		public List<XYCoordinateStruct> GetFOV(int x, int y, int viewDistance)
		{
			var viewSet = new HashSet<int> { ConvertXYToInt(x, y) };
			for (int octant = 0; octant < 8; octant++)
			{
				CastLight(x, y, 1, 1.0d, 0.0d, viewDistance, _octantTranslate[0, octant], _octantTranslate[1, octant],
						  _octantTranslate[2, octant], _octantTranslate[3, octant], 0, viewSet);
			}

			var returnList = new List<XYCoordinateStruct>();
			foreach (int index in viewSet)
				returnList.Add(ConvertIndexToXY(index));

			return returnList;
		}

		public List<Entities.Entity> GetConcealedEntity(List<XYCoordinateStruct> visibleTiles)
		{
			var concealedEntities = new List<Entities.Entity>();

			foreach (XYCoordinateStruct tile in visibleTiles)
			{
				// TODO: Add any other types here as required.
				var index = ConvertXYToInt(tile.X, tile.Y);
				if (HasFurnishing(index) && !GetFurnishing(index).PlayerSpotted)
					concealedEntities.Add(GetFurnishing(index));
				if (HasActor(index) && !GetActor(index).PlayerSpotted)
					concealedEntities.Add(GetActor(index));
			}

			return concealedEntities;
		}

		// TODO: Get the reference where this came from.
		void CastLight(int xLoc, int yLoc, int row, double start, double end, int viewDistance, int xx, int xy, int yx,
					   int yy, int recursionNumber, HashSet<int> viewSet)
		{
			if (start < end)
				return;

			var newStart = -1.0d;
			var viewDistanceSquared = viewDistance * viewDistance;

			for (int j = row; j < viewDistance + 1; j++)
			{
				var dx = -j - 1;
				var dy = -j;
				var blocked = false;

				while (dx <= 0)
				{
					dx += 1;

					var mapX = xLoc + dx * xx + dy * xy;
					var mapY = yLoc + dx * yx + dy * yy;

					var lSlope = (dx - 0.5) / (dy + 0.5);
					var rSlope = (dx + 0.5) / (dy - 0.5);

					if (start <= rSlope)
						continue;
					if (end >= lSlope)
						break;

					// We can see this square
					if (dy * dx + dy * dy < viewDistanceSquared && IsValidMapCoord(mapX, mapY))
						viewSet.Add(ConvertXYToInt(mapX, mapY));
					if (blocked)
					{
						// We are scanning blocked squares
						if (!IsValidMapCoord(mapX, mapY) || BlockLOS(mapX, mapY))
						{
							newStart = rSlope;
							continue;
						}
						blocked = false;
						start = newStart;
					}
					else if (!IsValidMapCoord(mapX, mapY) || BlockLOS(mapX, mapY))
					{
						// Start a child scan
						blocked = true;
						CastLight(xLoc, yLoc, j + 1, start, lSlope, viewDistance, xx, xy, yx, yy, 
						          recursionNumber + 1, viewSet);
						newStart = rSlope;
					}
				}
				// Row is scanned  do next row unless last square was blocked.
				if (blocked)
				{
					break;
				}
			}
		}

		public bool InSight(int originX, int originY, int destinationX, int destinationY, int viewDistance)
		{
			// Determine which octant we are in
			int octant = 0;
			if (Math.Abs(originY - destinationY) < Math.Abs(originX - destinationX))
				octant += 1;
			if (destinationX - originX > 0)
				octant = 3 - octant;
			if (destinationY - originY > 0)
				octant = 7 - octant;

			var viewSet = new HashSet<int>();

			CastLight(originX, originY, 1, 1.0, 0.0, viewDistance,
					  _octantTranslate[0, octant], _octantTranslate[1, octant],
					  _octantTranslate[2, octant], _octantTranslate[3, octant],
					  0, viewSet);
			viewSet.Add(ConvertXYToInt(originX, originY));
			return viewSet.Contains(ConvertXYToInt(destinationX, destinationY));
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
				return _actors[index];
			return null;
		}

		public void AddActor(Actor actor)
		{
			AddActor(actor, actor.XLoc, actor.YLoc);
		}

		public void AddActor(Actor actor, int xLoc, int yLoc)
		{
			var index = ConvertXYToInt(xLoc, yLoc);
			AddActor(actor, index);
		}

		void AddActor(Actor actor, int index)
		{
			if (_actors.ContainsKey(index))
			{
				// TODO: Add error text
			}
			_actors[index] = actor;
		}

		public void RemoveActor(Actor actor)
		{
			RemoveActor(actor, actor.XLoc, actor.YLoc);
		}

		public void RemoveActor(Actor actor, int xLoc, int yLoc)
		{
			var index = ConvertXYToInt(xLoc, yLoc);
			RemoveActor(actor, index);
		}

		void RemoveActor(Actor actor, int index)
		{
			if (_actors.ContainsKey(index))
			{
				if (_actors[index] == actor)
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

		// Furnishing functions
		public bool HasFurnishing(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return HasFurnishing(index);
		}

		bool HasFurnishing(int index)
		{
			return _furnishings.ContainsKey(index);
		}

		public Furnishing GetFurnishing(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return GetFurnishing(index);
		}

		Furnishing GetFurnishing(int index)
		{
			if (_furnishings.ContainsKey(index))
				return _furnishings[index];
			return null;
		}

		public void AddFurnishing(Furnishing furnishing)
		{
			AddFurnishing(furnishing, furnishing.XLoc, furnishing.YLoc);
		}

		public void AddFurnishing(Furnishing furnishing, int xLoc, int yLoc)
		{
			var index = ConvertXYToInt(xLoc, yLoc);
			AddFurnishing(furnishing, index);
		}

		void AddFurnishing(Furnishing furnishing, int index)
		{
			if (_furnishings.ContainsKey(index))
			{
				// TODO: Add error text here
			}
			_furnishings[index] = furnishing;
		}

		public void RemoveFurnishing(Furnishing furnishing)
		{
			RemoveFurnishing(furnishing, furnishing.XLoc, furnishing.YLoc);
		}

		public void RemoveFurnishing(Furnishing furnishing, int xLoc, int yLoc)
		{
			var index = ConvertXYToInt(xLoc, yLoc);
			RemoveFurnishing(furnishing, index);
		}

		void RemoveFurnishing(Furnishing furnishing, int index)
		{
			if (_furnishings.ContainsKey(index))
			{
				if (_furnishings[index] == furnishing)
					_furnishings.Remove(index);
				else
				{
					// TODO: Add error text.
				}
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

		XYCoordinateStruct ConvertIndexToXY(int index)
		{
			return new XYCoordinateStruct(index % _mapWidth, index / _mapWidth);
		}

		Dictionary<string, string> ParseOtherEntityParameters(string[] details, int startingIndex)
		{
			var parameterDictionary = new Dictionary<string, string>();
			for (int i = startingIndex; i < details.Length; i += 2)
				parameterDictionary[details[i]] = details[i + 1];

			return parameterDictionary;
		}

		// Save game related stuff
		public LevelSaveSummary GetSaveDetails()
		{
			var summary = new LevelSaveSummary();

			summary.LevelId = _levelId;
			summary.LevelName = _levelName;
			summary.Height = _mapHeight;
			summary.Width = _mapWidth;

			summary.Tiles = _tileGrid;
			foreach (KeyValuePair<int, MapTileDetails> tile in _tileInformation)
				summary.TileTypes[tile.Key] = tile.Value.TileType;
			summary.Revealed = _revealed;

			foreach (KeyValuePair<int, Furnishing> furnishing in _furnishings)
				summary.Furnishings[furnishing.Key] = furnishing.Value.FurnishingId;
			foreach (KeyValuePair<int, Actor> actor in _actors)
				summary.Actors[actor.Key] = actor.Value.ActorId;

			return summary;
		}
	}
}

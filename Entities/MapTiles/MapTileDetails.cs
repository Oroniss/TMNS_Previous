using System.Collections.Generic;
using System;
using RLEngine.Entities.EntityInterfaces;

namespace RLEngine.Entities.MapTiles
{
	[Serializable]
	public class MapTileDetails:IBackgroundDrawing,ITrait
	{
		static Dictionary<MapTile, MapTileDetails> mapTileDetails;

		readonly MapTile _tileType;
		readonly string _backgroundColor;
		readonly string _fogColor;
		readonly List<Trait> _traits;

		MapTileDetails(MapTile tileType, string backgroundColor, string fogColor, Trait[] traits)
		{
			_tileType = tileType;
			_backgroundColor = backgroundColor;
			_fogColor = fogColor;
			_traits = new List<Trait>();
			foreach (Trait trait in traits)
				_traits.Add(trait);
		}

		public MapTile TileType
		{
			get { return _tileType; }
		}

		public string BackgroundColor
		{
			get { return _backgroundColor; }
		}

		public string FogColor
		{
			get { return _fogColor; }
		}

		public bool HasTrait(Trait trait)
		{
			return _traits.Contains(trait);
		}

		public void AddTrait(Trait trait)
		{
			// TODO: Add error messages here.
		}

		public void RemoveTrait(Trait trait)
		{
			// TODO: Add error messages here.
		}

		public static MapTileDetails GetTileDetails(MapTile tileType)
		{
			if (!mapTileDetails.ContainsKey(tileType))
				mapTileDetails[tileType] = QueryTileDatabase(tileType);
			return mapTileDetails[tileType];
		}

		static MapTileDetails QueryTileDatabase(MapTile tileType)
		{
			// TODO: Fix this to query a db file at least.
			if (tileType == MapTile.TestTile1)
				return new MapTileDetails(tileType, "Blue", "LightBlue", new Trait[] { Trait.TestTrait1 });
			else
				return new MapTileDetails(tileType, "GreySeven", "Grey12", new Trait[] { Trait.TestTrait2 });
		}
	}
}

using System.Collections.Generic;
using System;
using RLEngine.Entities.EntityInterfaces;

namespace RLEngine.Entities.MapTiles
{
	[Serializable]
	public class MapTileDetails:IBackgroundDrawing,ITrait
	{
		static Dictionary<TileType, MapTileDetails> mapTileDetails = new Dictionary<TileType, MapTileDetails>();

		readonly TileType _tileType;
		readonly string _backgroundColor;
		readonly string _fogColor;
		readonly List<Trait> _traits;

		MapTileDetails(TileType tileType, string backgroundColor, string fogColor, Trait[] traits)
		{
			_tileType = tileType;
			_backgroundColor = backgroundColor;
			_fogColor = fogColor;
			_traits = new List<Trait>();
			foreach (Trait trait in traits)
				_traits.Add(trait);
		}

		public TileType TileType
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

		public static MapTileDetails GetTileDetails(TileType tileType)
		{
			if (!mapTileDetails.ContainsKey(tileType))
				mapTileDetails[tileType] = QueryTileDatabase(tileType);
			return mapTileDetails[tileType];
		}

		static MapTileDetails QueryTileDatabase(TileType tileType)
		{
			// TODO: Fix this to query a db file at least.
			if (tileType == TileType.TestTile1)
				return new MapTileDetails(tileType, "GraySeven", "GrayFour", new Trait[] { Trait.TestTrait2 });
			else
				return new MapTileDetails(tileType, "LightBlue", "Blue", new Trait[] { Trait.TestTrait1 });
		}
	}
}

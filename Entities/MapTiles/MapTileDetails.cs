using System.Collections.Generic;
using System;

namespace TMNS.Entities.MapTiles
{
	[Serializable]
	public class MapTileDetails
	{
		readonly TileType _tileType;
		readonly string _backgroundColor;
		readonly string _fogColor;
		readonly string _description;
		readonly string _moveOffFunction;
		readonly string _moveOnFunction;
		readonly List<Trait> _traits;

		public MapTileDetails(TileType tileType, string backgroundColor, string fogColor, string description,
		                      string moveOnFunction, string moveOffFunction, Trait[] traits)
		{
			_tileType = tileType;
			_backgroundColor = backgroundColor;
			_fogColor = fogColor;
			_description = description;
			_moveOnFunction = moveOnFunction;
			_moveOffFunction = moveOffFunction;
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

		public string Description
		{
			get { return _description; }
		}

		// TODO: Implement movement functions.

		public bool HasTrait(Trait trait)
		{
			return _traits.Contains(trait);
		}
	}
}

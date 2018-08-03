using System.Collections.Generic;
using System;
using TMNS.Entities.EntityInterfaces;

namespace TMNS.Entities.MapTiles
{
	[Serializable]
	public class MapTileDetails:IBackgroundDrawing,ITrait
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

		public void AddTrait(Trait trait)
		{
			ErrorLogger.AddDebugText(string.Format("Tried to add trait {0} to map tile {1}", trait, _tileType));
		}

		public void RemoveTrait(Trait trait)
		{
			ErrorLogger.AddDebugText(string.Format("Tried to remove trait {0} from map tile {1}", trait, _tileType));
		}
	}
}

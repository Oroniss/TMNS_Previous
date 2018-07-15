using System.Collections.Generic;
using System;
using RLEngine.Entities.EntityInterfaces;

namespace RLEngine.Entities.MapTiles
{
	[Serializable]
	public class MapTileDetails:IBackgroundDrawing,ITrait
	{
		readonly TileType _tileType;
		readonly string _backgroundColor;
		readonly string _fogColor;
		readonly List<Trait> _traits;

		public MapTileDetails(TileType tileType, string backgroundColor, string fogColor, Trait[] traits)
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
			ErrorLogger.AddDebugText(string.Format("Tried to add trait {0} to map tile {1}", trait, _tileType));
		}

		public void RemoveTrait(Trait trait)
		{
			ErrorLogger.AddDebugText(string.Format("Tried to remove trait {0} from map tile {1}", trait, _tileType));
		}
	}
}

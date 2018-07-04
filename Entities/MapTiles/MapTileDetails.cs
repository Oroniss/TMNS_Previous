// Finished for version 0.1 - no change for version 0.2.

using System.Collections.Generic;
using System;
using RLEngine.Entities.EntityInterfaces;
using System.IO;

namespace RLEngine.Entities.MapTiles
{
	[Serializable]
	public class MapTileDetails:IBackgroundDrawing,ITrait
	{
		static string dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Entities", "MapTiles", "TileTypes.txt");

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
			ErrorLogger.AddDebugText(string.Format("Tried to add trait {0} to map tile {1}", trait, _tileType));
		}

		public void RemoveTrait(Trait trait)
		{
			ErrorLogger.AddDebugText(string.Format("Tried to remove trait {0} from map tile {1}", trait, _tileType));
		}

		public static MapTileDetails GetTileDetails(TileType tileType)
		{
			//TODO: Not spending too much time here since it's a placeholder.
			//TODO: Turn into better code when it hits the actual DB.

			var tileFile = new StreamReader(dataFilePath);

			var tileName = tileType.ToString();
			string line;
			MapTileDetails returnVal = null;

			while ((line = tileFile.ReadLine()) != null)
			{
				var splitLine = line.Trim().Split(',');
				if (splitLine[0] == tileName)
				{
					var traitLine = splitLine[3].Split('|');
					var traits = new Trait[traitLine.Length];
					for (int i = 0; i < traits.Length; i++)
						traits[i] = (Trait)Enum.Parse(typeof(Trait), traitLine[i]);

					returnVal = new MapTileDetails(tileType, splitLine[1], splitLine[2], traits);
					break;
				}
			}

			tileFile.Close();

			if (returnVal == null)
			{
				ErrorLogger.AddDebugText(string.Format("Couldn't find db entry for TileType {0}", tileType));
				return GetTileDetails(TileType.TestTile1);
			}
			return returnVal;
		}

		public static void SetTestFilePath(string testContext)
		{
			dataFilePath = Path.Combine(testContext, "Entities", "MapTiles", "TileTypes.txt");
		}
	}
}

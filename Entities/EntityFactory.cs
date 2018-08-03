using System.Collections.Generic;
using TMNS.Entities.Furnishings;
using TMNS.Entities.MapTiles;
using TMNS.Entities.Monsters;
using System;

namespace TMNS.Entities
{
	public static class EntityFactory
	{
		static Dictionary<string, FurnishingDetails> furnishings = new Dictionary<string, FurnishingDetails>();
		static Dictionary<TileType, MapTileDetails> mapTiles = new Dictionary<TileType, MapTileDetails>();
		static Dictionary<string, MonsterDetails> monsters = new Dictionary<string, MonsterDetails>();

		public static Furnishing CreateFurnishing(string furnishingName, int xLoc, int yLoc, Dictionary<string, string>
		                                         otherParameters)
		{
			if (!furnishings.ContainsKey(furnishingName))
				furnishings[furnishingName] = StaticDatabase.StaticDatabaseConnection.GetFurnishingDetails(furnishingName);

			return new Furnishing(furnishings[furnishingName], xLoc, yLoc, otherParameters);
		}

		public static MapTileDetails CreateMapTile(string tileType)
		{
			return CreateMapTile((TileType)Enum.Parse(typeof(TileType), tileType));
		}

		public static MapTileDetails CreateMapTile(TileType tileType)
		{
			if (!mapTiles.ContainsKey(tileType))
				mapTiles[tileType] = StaticDatabase.StaticDatabaseConnection.GetMapTileDetails(tileType);

			return mapTiles[tileType];
		}

		public static Monster CreateMonster(string monsterName, int xLoc, int yLoc, Dictionary<string, string>
												   otherParameters)
		{
			if (!monsters.ContainsKey(monsterName))
				monsters[monsterName] = StaticDatabase.StaticDatabaseConnection.GetMonsterDetails(monsterName);

			return new Monster(monsters[monsterName], xLoc, yLoc, otherParameters);
		}
	}
}

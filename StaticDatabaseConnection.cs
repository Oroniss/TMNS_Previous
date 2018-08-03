using System.IO;
using Mono.Data.Sqlite;
using TMNS.Entities.Furnishings;
using TMNS.Entities.Monsters;
using TMNS.Entities.MapTiles;
using TMNS.Entities;
using System;

namespace TMNS.StaticDatabase
{
	public static class StaticDatabaseConnection
	{
		static string databasePath = Path.Combine(Directory.GetCurrentDirectory(), "StaticDatabase.db");
		static SqliteConnection connection;
		static bool connectionIsOpen;


		public static void OpenDBConnection()
		{
			if (connectionIsOpen)
			{
				ErrorLogger.AddDebugText("Tried to reopen db connection");
				return;
			}
			connection = new SqliteConnection("Data Source=" + databasePath);
			connection.Open();
			connectionIsOpen = true;
		}

		public static void CloseDBConnection()
		{
			if (connectionIsOpen)
			{
				connection.Close();
				connectionIsOpen = false;
				return;
			}
			ErrorLogger.AddDebugText("Tried to re-close db connection");
		}

		public static FurnishingDetails GetFurnishingDetails(string furnishingName)
		{
			string queryText = string.Format("SELECT * FROM Furnishings WHERE FurnishingName = \"{0}\";", furnishingName);

			using (var queryCommand = connection.CreateCommand())
			{
				queryCommand.CommandText = queryText;

				var reader = queryCommand.ExecuteReader();

				if (reader.Read())
				{
					char symbol = reader.GetString(1)[0];
					string fgColorName = reader.GetString(2);
					string description = SafeGetString(reader, 3);
					Material material = (Material)Enum.Parse(typeof(Material), reader.GetString(4));
					Trait[] traits = ParseTraits(SafeGetString(reader,5));

					return new FurnishingDetails(furnishingName, symbol, fgColorName, description, material, traits);
				}
			}

			ErrorLogger.AddDebugText("Unknown furnishing: " + furnishingName);
			return GetFurnishingDetails("Circle");
		}

		public static MonsterDetails GetMonsterDetails(string monsterName)
		{
			string queryText = string.Format("SELECT * FROM Monsters WHERE MonsterName = \"{0}\";", monsterName);

			using (var queryCommand = connection.CreateCommand())
			{
				queryCommand.CommandText = queryText;

				var reader = queryCommand.ExecuteReader();

				if (reader.Read())
				{
					char symbol = reader.GetString(1)[0];
					string fgColorName = reader.GetString(2);
					Trait[] traits = ParseTraits(reader.GetString(3));

					return new MonsterDetails(monsterName, symbol, fgColorName, traits);
				}
			}

			ErrorLogger.AddDebugText("Unknown monster: " + monsterName);
			return GetMonsterDetails("TestMonster1");
			
		}

		public static MapTileDetails GetMapTileDetails(string mapTileName)
		{
			string queryText = string.Format("SELECT * FROM MapTiles WHERE TileType = \"{0}\";", mapTileName);

			using (var queryCommand = connection.CreateCommand())
			{
				queryCommand.CommandText = queryText;

				var reader = queryCommand.ExecuteReader();

				if (reader.Read())
				{
					TileType tileType = (TileType)Enum.Parse(typeof(TileType), reader.GetString(0));
					string bgColorName = reader.GetString(1);
					string fogColorName = reader.GetString(2);
					string description = SafeGetString(reader, 3);
					string moveOnFunction = SafeGetString(reader, 4);
					string moveOffFunction = SafeGetString(reader, 5);
					Trait[] traits = ParseTraits(SafeGetString(reader, 6));

					return new MapTileDetails(tileType, bgColorName, fogColorName, description, moveOnFunction,
											  moveOffFunction, traits);
				}
			}

			ErrorLogger.AddDebugText("Unknown map tile type: " + mapTileName);
			return GetMapTileDetails("StoneFloor");
		}

		public static MapTileDetails GetMapTileDetails(TileType tileType)
		{
			return GetMapTileDetails(tileType.ToString());
		}

		static string SafeGetString(SqliteDataReader reader, int columnNumber)
		{
			if (!reader.IsDBNull(columnNumber))
				return reader.GetString(columnNumber);
			return string.Empty;
		}

		static Trait[] ParseTraits(string dbField)
		{
			if (dbField == "")
				return new Trait[0];
			var splitTraits = dbField.Split('|');
			var traits = new Trait[splitTraits.Length];
			for (int i = 0; i < splitTraits.Length; i++)
				traits[i] = (Trait)Enum.Parse(typeof(Trait), splitTraits[i]);
			return traits;
		}

		public static void SetToTest(string testDBPath)
		{
			databasePath = Path.Combine(testDBPath, "StaticDatabase.db");
		}
	}
}

using System.IO;
using Mono.Data.Sqlite;
using RLEngine.Entities.Furnishings;
using RLEngine.Entities.MapTiles;
using RLEngine.Entities;
using System;

namespace RLEngine.StaticDatabase
{
	public static class StaticDatabaseConnection
	{
		static string databasePath = Path.Combine(Directory.GetCurrentDirectory(), "StaticDatabase.db");
		static SqliteConnection connection;
		static bool connectionIsOpen;
		static bool inTestMode;


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
					Trait[] traits = ParseTraits(reader.GetString(3));

					return new FurnishingDetails(furnishingName, symbol, fgColorName, traits);
				}
			}

			ErrorLogger.AddDebugText("Unknown furnishing: " + furnishingName);
			return null;
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
					Trait[] traits = ParseTraits(reader.GetString(3));

					return new MapTileDetails(tileType, bgColorName, fogColorName, traits);
				}
			}

			ErrorLogger.AddDebugText("Unknown map tile type: " + mapTileName);
			return null;
		}

		public static MapTileDetails GetMapTileDetails(TileType tileType)
		{
			return GetMapTileDetails(tileType.ToString());
		}

		static Trait[] ParseTraits(string dbField)
		{
			if (dbField == null)
				return new Trait[0];
			var splitTraits = dbField.Split('|');
			var traits = new Trait[splitTraits.Length];
			for (int i = 0; i < splitTraits.Length; i++)
				traits[i] = (Trait)Enum.Parse(typeof(Trait), splitTraits[i]);
			return traits;
		}

		public static void SetToTest(string testDBPath)
		{
			databasePath = testDBPath;
			inTestMode = true;
		}
	}
}

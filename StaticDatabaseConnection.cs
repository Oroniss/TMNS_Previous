using System.IO;
using Mono.Data.Sqlite;
using RLEngine.Entities.Furnishings;
using RLEngine.Entities.MapTiles;

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
			return null;
		}

		public static MapTileDetails GetMapTileDetails(string mapTileName)
		{
			return null;
		}

		public static MapTileDetails GetMapTileDetails(TileType tileType)
		{
			return GetMapTileDetails(tileType.ToString());
		}

		public static void SetToTest(string testDBPath)
		{
			databasePath = testDBPath;
			inTestMode = true;
		}
	}
}

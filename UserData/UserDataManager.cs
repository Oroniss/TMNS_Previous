using TMNS.UserData;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace TMNS
{
	// TODO: Put this in application settings.
	public static class UserDataManager
	{
		static string _homeDirectory = Directory.GetCurrentDirectory();
		static string _configFilePath;
		static string _saveSummaryFilePath;
		static string _saveFileFolder;
		static string _logFileFolder;

		static readonly SortedDictionary<int, SaveGameSummary> _defaultSaveSummary = 
			new SortedDictionary<int, SaveGameSummary>();

		// Initial setup
		public static void SetupDirectoriesAndFiles()
		{
			SetFilePaths();

			if (!Directory.Exists(Path.Combine(_homeDirectory, "UserData")))
				Directory.CreateDirectory(Path.Combine(_homeDirectory, "UserData"));
			if (!Directory.Exists(_saveFileFolder))
				Directory.CreateDirectory(_saveFileFolder);
			if (!Directory.Exists(_logFileFolder))
				Directory.CreateDirectory(_logFileFolder);

			if (!File.Exists(_saveSummaryFilePath))
				CreateNewSaveSummaryFile();
		}

		static void SetFilePaths()
		{
			_configFilePath = Path.Combine(_homeDirectory, "UserData", "config.gen");
			_saveSummaryFilePath = Path.Combine(_homeDirectory, "UserData", "SSF.gen");
			_saveFileFolder = Path.Combine(_homeDirectory, "UserData", "Saves");
			_logFileFolder = Path.Combine(_homeDirectory, "UserData", "Logs");
		}

		static void CreateNewSaveSummaryFile()
		{
			WriteSummaryFile(_defaultSaveSummary);
		}


		// Save and load game.
		public static void WriteSaveGameSummary(SaveGameSummary summary)
		{
			var saveSummaries = ReadSummaryFile();
			saveSummaries[summary.GameData.GameID] = summary;
			WriteSummaryFile(saveSummaries);
		}

		public static void SaveGame(SaveGame gameState)
		{
			WriteSaveGameSummary(gameState.Summary);
			var filePath = Path.Combine(_saveFileFolder, string.Format("GID{0}.gen", 
			                                                           gameState.Summary.GameData.GameID));
			var fileStream = File.OpenWrite(filePath);
			var serialiser = new BinaryFormatter();
			serialiser.Serialize(fileStream, gameState);
			fileStream.Close();
		}

		public static SaveGame GetGameState(int gameID)
		{
			SaveGame gameState = null;
			var summary = ReadSummaryFile()[gameID];
			// TODO: Perform a check to see if the character is actually dead - also consider GM options, etc.
			if (summary.CurrentLevelName == "NEWGAME")
			{
				gameState = new SaveGame(summary);
			}
			else
			{
				var filePath = Path.Combine(_saveFileFolder, string.Format("GID{0}.gen", summary.GameData.GameID));
				var fileStream = File.OpenRead(filePath);
				var serialiser = new BinaryFormatter();
				gameState = (SaveGame)serialiser.Deserialize(fileStream);
				fileStream.Close();
			}
			return gameState;
		}

		public static SaveGameSummary GetSummary(int gameId)
		{
			var saveSummaries = ReadSummaryFile();
			if (saveSummaries.ContainsKey(gameId))
				return saveSummaries[gameId];
			return null;
		}

		public static void DeleteSaveGame(int gameID)
		{
			var filePath = Path.Combine(_saveFileFolder, string.Format("GID{0}.gen", gameID));
			if (File.Exists(filePath))
				File.Delete(filePath);
		}

		static SortedDictionary<int, SaveGameSummary> ReadSummaryFile()
		{
			var fileStream = File.OpenRead(_saveSummaryFilePath);
			var serialiser = new BinaryFormatter();
			var saveSummaries = (SortedDictionary<int, SaveGameSummary>)serialiser.Deserialize(fileStream);
			fileStream.Close();
			return saveSummaries;
		}

		static void WriteSummaryFile(SortedDictionary<int, SaveGameSummary> saveSummaries)
		{
			var fileStream = File.OpenWrite(_saveSummaryFilePath);
			var serialiser = new BinaryFormatter();
			serialiser.Serialize(fileStream, saveSummaries);
			fileStream.Close();
		}

		// Testing functionality
		public static void SetTestHomeDirectory(string testLocation)
		{
			_homeDirectory = testLocation;
		}

		public static void DeleteSaveSummaryFile()
		{
			// Only to be used for testing - will erase all user save games
			if (File.Exists(_saveSummaryFilePath))
				File.Delete(_saveSummaryFilePath);
		}
	}
}

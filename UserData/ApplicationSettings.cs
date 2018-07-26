using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Collections.Generic;

namespace RLEngine.UserData
{
	public static class ApplicationSettings
	{
		static readonly int NUMBEROFSAVESLOTS = 20;

		static ISettings CurrentParameters
		{
			get { return CrossSettings.Current; }
		}

		// General configuration settings
		public static bool ExtraKeys
		{
			get { return CurrentParameters.GetValueOrDefault("ExtraKeys", false); }
			set { CurrentParameters.AddOrUpdateValue("ExtraKeys", value); 
			}
		}

		public static bool FullLogging
		{
			get { return CurrentParameters.GetValueOrDefault("FullLogging", false); }
			set { CurrentParameters.AddOrUpdateValue("FullLogging", value); }
		}

		public static bool GMOptions
		{
			get { return CurrentParameters.GetValueOrDefault("GMOptions", false); }
			set { CurrentParameters.AddOrUpdateValue("GMOptions", value); }
		}

		// Save game related settings
		public static int GenerateNextGameId()
		{
			if (!HasFreeSaveSlot())
			{
				ErrorLogger.AddDebugText("Can't create new save game");
				return -1;
			}

			var newGameId = CurrentParameters.GetValueOrDefault("LastGameId", 0);
			newGameId++;
			CurrentParameters.AddOrUpdateValue("LastGameId", newGameId);
			AddSaveGame(newGameId);
			return newGameId;
		}

		public static bool HasFreeSaveSlot()
		{
			var saveIds = ReadSaveSlots();
			// Just need to check the last slot
			return saveIds[NUMBEROFSAVESLOTS - 1] == -1;
		}

		public static List<int> GetSaveGameIds()
		{
			var saveIds = ReadSaveSlots();
			var returnIds = new List<int>();
			for (int i = 0; i < saveIds.Length; i++)
			{
				if (saveIds[i] != -1)
					returnIds.Add(saveIds[i]);
			}
			return returnIds;
		}

		public static void RemoveSaveGame(int gameId)
		{
			var saveIds = ReadSaveSlots();
			int removeAt = 0;

			for (int i = 0; i < saveIds.Length; i++)
			{
				if (saveIds[i] == gameId)
				{
					removeAt = i;
					saveIds[i] = -1;
					break;
				}
			}

			for (int i = saveIds.Length - 1; i > removeAt; i--)
			{
				if (saveIds[i] != -1)
				{
					saveIds[removeAt] = saveIds[i];
					saveIds[i] = -1;
					break;
				}
			}

			WriteSaveSlots(saveIds);
		}

		public static void AddSaveGame(int gameId)
		{
			var saveIds = ReadSaveSlots();
			for (int i = 0; i < saveIds.Length; i++)
			{
				if (saveIds[i] == -1)
				{
					saveIds[i] = gameId;
					break;
				}
			}

			WriteSaveSlots(saveIds);
		}

		static int[] ReadSaveSlots()
		{
			int[] saveIds = new int[NUMBEROFSAVESLOTS];

			for (int i = 0; i < NUMBEROFSAVESLOTS; i++)
			{
				saveIds[i] = CurrentParameters.GetValueOrDefault(string.Format("SaveSlot{0}", i), -1);
			}

			return saveIds;
		}

		static void WriteSaveSlots(int[] saveIds)
		{
			for (int i = 0; i < saveIds.Length; i++)
			{
				CurrentParameters.AddOrUpdateValue(string.Format("SaveSlot{0}", i), saveIds[i]);
			}
		}

		// Note, mostly present for testing purposes.
		public static void ClearAllParameters()
		{
			CurrentParameters.Clear();
		}
	}
}

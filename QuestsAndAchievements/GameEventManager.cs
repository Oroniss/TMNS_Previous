// Tidied up for version 0.3.

using TMNS.GameEvents;

namespace TMNS.Quests
{
	public static class GameEventManager
	{
		static StatisticsManager statisticsManager = new StatisticsManager();

		public static void SetupGameEventHandling()
		{
			GameEvent.OnGameEvent +=  statisticsManager.ProcessEvent;
		}

		public static void SaveData(UserData.SaveGame saveGame)
		{
			saveGame.CurrentStatistics = statisticsManager;

			// TODO: Add achievement and quest manageres here.
		}

		public static void LoadData(UserData.SaveGame saveGame)
		{
			statisticsManager = saveGame.CurrentStatistics;

			// TODO: Add achievement and quest managers here.

			SetupGameEventHandling();
		}
	}
}

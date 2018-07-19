using RLEngine.GameEvents;

namespace RLEngine.Quests
{
	public static class GameEventManager
	{
		static StatisticsManager statisticsManager = new StatisticsManager();

		public static void Setup()
		{
			GameEvent.OnGameEvent +=  statisticsManager.ProcessEvent;
		}

		public static void LoadData(UserData.SaveGame saveGame)
		{
			statisticsManager = saveGame.CurrentStatistics;

			// TODO: Add achievement and quest managers here.

			Setup();
		}

		public static void SaveData(UserData.SaveGame saveGame)
		{
			saveGame.CurrentStatistics = statisticsManager;

			// TODO: Add achievement and quest manageres here.
		}
	}
}

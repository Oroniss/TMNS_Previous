// Tidied up for version 0.2.

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
	}
}

using RLEngine.GameEvents;

namespace RLEngine.Quests
{
	public static class StatisticsManager
	{
		static int numberOfMoves = 0;


		public static void Setup()
		{
			GameEventManager.OnGameEvent += ProcessEvent;
		}

		public static void ProcessEvent(object sender, GameEventArgs gameEvent)
		{
			if (gameEvent.EventType == EventType.MovementEvent)
			{
				numberOfMoves++;
				if (numberOfMoves % 20 == 0)
					MainGraphicDisplay.TextConsole.AddOutputText(string.Format("Made {0} moves", numberOfMoves));
			}
		}
	}
}

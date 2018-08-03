// Tidied up for version 0.2 - no changes for 0.3.

namespace TMNS.Quests
{
	public interface IGameEventProcessor
	{
		void ProcessEvent(object sender, GameEvents.GameEvent args);
	}
}

// Tidied up for version 0.2.

namespace RLEngine.Quests
{
	public interface IGameEventProcessor
	{
		void ProcessEvent(object sender, GameEvents.GameEvent args);
	}
}

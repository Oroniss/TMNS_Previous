// Tidied up for version 0.2.

using RLEngine.GameEvents;
using System;

namespace RLEngine.Quests
{
	[Serializable]
	public class StatisticsManager:IGameEventProcessor
	{
		long stepsTaken = 0;

		public void ProcessEvent(object sender, GameEvent gameEvent)
		{
			switch (gameEvent.EventType)
			{
				case EventType.MovementEvent:
					{
						var moveEvent = (MoveActorEvent)gameEvent;

						if (moveEvent.Actor.HasTrait(Entities.Trait.Player))
						{
							stepsTaken++;
							if (stepsTaken % 20 == 0)
								MainGraphicDisplay.TextConsole.AddOutputText(string.Format("Taken {0} steps", stepsTaken));
						}
						break;
					}
			}
		}
	}
}

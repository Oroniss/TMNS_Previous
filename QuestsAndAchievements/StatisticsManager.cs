// Tidied up for version 0.2 - no changes for version 0.3.

using TMNS.GameEvents;
using System;

namespace TMNS.Quests
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

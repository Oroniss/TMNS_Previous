using System.Collections.Generic;
using System;

namespace RLEngine.GameEvents
{
	public abstract class GameEvent
	{
		static List<GameEvent> eventQueue = new List<GameEvent>();

		readonly EventType _eventType;

		protected GameEvent(EventType eventType)
		{
			_eventType = eventType;
		}

		public EventType EventType
		{
			get { return _eventType; }
		}

		protected static void AddEvent(GameEvent gameEvent)
		{
			eventQueue.Add(gameEvent);
		}

		public static void ProcessEvents()
		{
			while (eventQueue.Count > 0)
			{
				// TODO: Process event needs to go on the Achievement/Stats class - possibly also the AI class.
				Quests.QuestAndAchievementHandler.ProcessEvent(eventQueue[0]);
				eventQueue.RemoveAt(0);
			}
		}
	}
}

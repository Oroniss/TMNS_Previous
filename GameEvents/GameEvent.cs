// Tidied up for version 0.2.

using System;

namespace RLEngine.GameEvents
{
	public abstract class GameEvent:EventArgs
	{
		public static event EventHandler<GameEvent> OnGameEvent;

		readonly EventType _eventType;

		protected GameEvent(EventType eventType)
		{
			_eventType = eventType;
		}

		public EventType EventType
		{
			get { return _eventType; }
		}

		protected static void PublishGameEvent(object sender, GameEvent args)
		{
			OnGameEvent(sender, args);
		}
	}
}

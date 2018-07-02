using System;

namespace RLEngine.GameEvents
{
	public abstract class GameEventArgs:EventArgs
	{
		readonly EventType _eventType;

		protected GameEventArgs(EventType eventType)
		{
			_eventType = eventType;
		}

		public EventType EventType
		{
			get { return _eventType; }
		}
	}
}

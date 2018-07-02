using System;
using RLEngine.Entities.Actors;

namespace RLEngine.GameEvents
{
	public static class GameEventManager
	{
		public static event EventHandler<GameEventArgs> OnGameEvent;

		public static void GenerateNewMovementEvent(Actor actor, int deltaX, int deltaY)
		{
			OnGameEvent(actor, new MoveActorEventArgs(actor, deltaX, deltaY));
		}
	}
}

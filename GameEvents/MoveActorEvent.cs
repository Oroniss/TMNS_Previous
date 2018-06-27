using RLEngine.Entities.Actors;

namespace RLEngine.GameEvents
{
	public class MoveActorEvent:GameEvent
	{
		public Actor Actor;
		public int OriginX;
		public int OriginY;
		public int DestinationX;
		public int DestinationY;
		public MovementModes MovementMode;

		MoveActorEvent(Actor actor, int originX, int originY, int destinationX, int destinationY, MovementModes movementMode)
			:base(EventType.MovementEvent)
		{
			Actor = actor;
			OriginX = originX;
			OriginY = originY;
			DestinationX = destinationX;
			DestinationY = destinationY;
			MovementMode = movementMode;
		}

		public static void GenerateMoveActorEvent(Actor actor, int deltaX, int deltaY)
		{
			GenerateMoveActorEvent(actor, deltaX, deltaY, MovementModes.Walk);
		}

		public static void GenerateMoveActorEvent(Actor actor, int deltaX, int deltaY, MovementModes movementMode)
		{
			var moveEvent = new MoveActorEvent(actor, actor.XLoc, actor.YLoc, actor.XLoc + deltaX, actor.YLoc + deltaY, movementMode);
			AddEvent(moveEvent);
			
		}
	}
}

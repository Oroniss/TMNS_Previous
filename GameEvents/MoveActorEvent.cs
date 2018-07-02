// Tidied up for version 0.2.

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

		MoveActorEvent(Actor actor, int originX, int originY, int destinationX, int destinationY, 
		                      MovementModes movementMode)
			:base(EventType.MovementEvent)
		{
			Actor = actor;
			OriginX = originX;
			OriginY = originY;
			DestinationX = destinationX;
			DestinationY = destinationY;
			MovementMode = movementMode;
		}

		public static void GenerateMoveActorEvent(Actor actor, int originX, int originY, int destinationX, 
		                                          int destinationY, MovementModes movementMode)
		{

			var moveActorEvent = new MoveActorEvent(actor, originX, originY, destinationX, destinationY, movementMode);
			PublishGameEvent(actor, moveActorEvent);
		}

		public static void GenerateMoveActorEvent(Actor actor, int originX, int originY, int destinaionX,
												  int destinationY)
		{
			GenerateMoveActorEvent(actor, originX, originY, destinaionX, destinationY, MovementModes.Walk);
		}

		public static void GenerateMoveActorEvent(Actor actor, int deltaX, int deltaY, MovementModes movementMode)
		{
			GenerateMoveActorEvent(actor, actor.XLoc, actor.YLoc, actor.XLoc + deltaX, actor.YLoc + deltaY,
								   movementMode);
		}

		public static void GenerateMoveActorEvent(Actor actor, int deltaX, int deltaY)
		{
			GenerateMoveActorEvent(actor, actor.XLoc, actor.YLoc, actor.XLoc + deltaX, actor.YLoc + deltaY,
								   MovementModes.Walk);
		}
	}
}

using RLEngine.Entities.Actors;

namespace RLEngine.GameEvents
{
	public class MoveActorEventArgs:GameEventArgs
	{
		public Actor Actor;
		public int OriginX;
		public int OriginY;
		public int DestinationX;
		public int DestinationY;
		public MovementModes MovementMode;

		public MoveActorEventArgs(Actor actor, int originX, int originY, int destinationX, int destinationY, MovementModes movementMode)
			:base(EventType.MovementEvent)
		{
			Actor = actor;
			OriginX = originX;
			OriginY = originY;
			DestinationX = destinationX;
			DestinationY = destinationY;
			MovementMode = movementMode;
		}

		public MoveActorEventArgs(Actor actor, int originX, int originY, int destinationX, int destinationY)
			: this(actor, originX, originY, destinationX, destinationY, MovementModes.Walk) { }

		public MoveActorEventArgs(Actor actor, int deltaX, int deltaY, MovementModes movementMode)
			: this(actor, actor.XLoc, actor.YLoc, actor.XLoc + deltaX, actor.YLoc + deltaY, movementMode) { }

		public MoveActorEventArgs(Actor actor, int deltaX, int deltaY)
			: this(actor, deltaX, deltaY, MovementModes.Walk) { }
	}
}

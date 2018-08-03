using TMNS.Entities.Actors;

namespace TMNS.GameEvents
{
	public class MoveActorEvent:GameEvent
	{
		public Actor Actor;
		public int OriginX;
		public int OriginY;
		public int DestinationX;
		public int DestinationY;

		MoveActorEvent(Actor actor, int originX, int originY, int destinationX, int destinationY)
			:base(EventType.MovementEvent)
		{
			Actor = actor;
			OriginX = originX;
			OriginY = originY;
			DestinationX = destinationX;
			DestinationY = destinationY;
		}

		public static void GenerateMoveActorEvent(Actor actor, int originX, int originY, int destinationX, 
		                                          int destinationY)
		{
			var moveActorEvent = new MoveActorEvent(actor, originX, originY, destinationX, destinationY);
			PublishGameEvent(actor, moveActorEvent);
		}

		public static void GenerateMoveActorEvent(Actor actor, int deltaX, int deltaY)
		{
			GenerateMoveActorEvent(actor, actor.XLoc, actor.YLoc, actor.XLoc + deltaX, actor.YLoc + deltaY);
		}
	}
}

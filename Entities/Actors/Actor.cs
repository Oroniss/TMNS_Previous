using System;
using System.Collections.Generic;

namespace RLEngine.Entities.Actors
{
	[Serializable]
	public abstract class Actor:Entity
	{
		static Trait[] actorTraits = {Trait.Actor, Trait.BlockMove };

		protected int _actorId;

		protected int _nextMove;
		protected int _viewDistance;

		protected Actor(ActorDetails details, int xLoc, int yLoc, Dictionary<string, string> otherParameters)
			: base(details, xLoc, yLoc, otherParameters)
		{
			_viewDistance = 12; // TODO: Put this on a template somewhere.

			foreach (Trait trait in actorTraits)
				AddTrait(trait);
		}

		public int ActorId
		{
			get { return _actorId; }
		}

		public int ViewDistance
		{
			get { return _viewDistance; }
		}

		public void UpdatePosition(int newX, int newY)
		{
			_xLoc = newX;
			_yLoc = newY;
		}

		public void Move(int deltaX, int deltaY)
		{
			_xLoc += deltaX;
			_yLoc += deltaY;
		}

		// TODO: Decide whether this needs to be public for the AI module.
		protected virtual bool MakeMoveAttempt(Levels.Level currentLevel, int deltaX, int deltaY)
		{
			// TODO: Apply movement functions here.

			// TODO: Add movement mode when appropriate
			// TODO: Also make sure this can always be done in advance, i.e. nothing that follows can change this.
			GameEvents.MoveActorEvent.GenerateMoveActorEvent(this, deltaX, deltaY);

			currentLevel.RemoveActor(this);
			Move(deltaX, deltaY);
			currentLevel.AddActor(this);

			return true;
		}

		protected abstract void GetNextMove(Levels.Level currentLevel);

		public override void Update(Levels.Level currentLevel)
		{
			base.Update(currentLevel);

			if (!_destroyed)
				GetNextMove(currentLevel);
		}

		public static Actor GetActor(int actorId)
		{
			if (actorId == 0)
				return Player.Player.GetPlayer();

			return Monsters.Monster.GetMonster(actorId);
		}
	}
}

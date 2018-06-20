using System;
using System.Collections.Generic;

namespace RLEngine.Entities.Actors
{
	[Serializable]
	public abstract class Actor:Entity
	{
		static SortedDictionary<int, Actor> actors = new SortedDictionary<int, Actor>();

		static int currentMaxId = 1;
		static List<int> freeActorIds = new List<int>();

		int _actorId;

		protected int _nextMove;

		protected Actor(string entityName, int xLoc, int yLoc, Dictionary<string, string> otherParameters)
			: base(entityName, xLoc, yLoc, otherParameters)
		{
			if (entityName == "Player")
				_actorId = 0;
			else
			{
				if (freeActorIds.Count > 0)
				{
					_actorId = freeActorIds[0];
					freeActorIds.RemoveAt(0);
				}
				else
				{
					_actorId = currentMaxId;
					currentMaxId++;
				}
				actors[_actorId] = this;
			}
		}

		public int ActorId
		{
			get { return _actorId; }
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

			currentLevel.RemoveActor(this);
			Move(deltaX, deltaY);
			currentLevel.AddActor(this);

			return true;

		}

		protected abstract void GetNextMove(Levels.Level currentLevel);

		public override void Update(Levels.Level currentLevel)
		{
			base.Update(currentLevel);

			GetNextMove(currentLevel);
		}

		public static Actor GetActor(int actorId)
		{
			if (actorId == 0)
			{
				return Player.Player.GetPlayer();
			}
			else
			{
				if (actors.ContainsKey(actorId))
					return actors[actorId];
				else
				{
					ErrorLogger.AddDebugText(string.Format("Tried to get unknown actor ID: {0}", actorId));
					return null;
				}
			}
		}

		public static void UpdateActors(Levels.Level currentLevel)
		{
			foreach (KeyValuePair<int, Actor> actor in actors)
				actor.Value.Update(currentLevel);
		}
	}
}

// Tidied for  version 0.3.

using System;
using RLEngine.Levels;
using System.Collections.Generic;
using RLEngine.Entities.Actors;

namespace RLEngine.Entities.Monsters
{
	[Serializable]
	public class Monster:Actor
	{
		protected static SortedDictionary<int, Monster> monsters = new SortedDictionary<int, Monster>();

		protected static int currentMaxId = 1;
		protected static List<int> freeMonsterIds = new List<int>();

		public Monster(EntityBasicDetails details, int xLoc, int yLoc, Dictionary<string, string> otherParameters)
			: base(details, xLoc, yLoc, otherParameters)
		{
				if (freeMonsterIds.Count > 0)
				{
					_actorId = freeMonsterIds[0];
					freeMonsterIds.RemoveAt(0);
				}
				else
				{
					_actorId = currentMaxId;
					currentMaxId++;
				}
				monsters[_actorId] = this;
		}

		public override void Dispose()
		{
			base.Dispose();

			// TODO: Make sure this get's removed from the timer if needed.

			monsters.Remove(_actorId);
			freeMonsterIds.Add(_actorId);	
		}

		protected override void GetNextMove(Level currentLevel)
		{
			throw new NotImplementedException();
		}

		public static Monster GetMonster(int actorId)
		{
			if (monsters.ContainsKey(actorId))
				return monsters[actorId];

			ErrorLogger.AddDebugText(string.Format("Tried to get unknown actor ID: {0}", actorId));
			return null;
		}

		public static void UpdateMonsters(Level currentLevel)
		{
			foreach (KeyValuePair<int, Monster> monster in monsters)
				monster.Value.Update(currentLevel);
		}

		public static MonsterSaveDetails GetSaveData()
		{
			var data = new MonsterSaveDetails();

			data.CurrentMaxId = currentMaxId;
			data.FreeActorIds = freeMonsterIds;
			data.CurrentMonsters = monsters;

			return data;
		}

		public static void LoadSaveData(MonsterSaveDetails data)
		{
			currentMaxId = data.CurrentMaxId;
			freeMonsterIds = data.FreeActorIds;
			monsters = data.CurrentMonsters;
		}
	}
}

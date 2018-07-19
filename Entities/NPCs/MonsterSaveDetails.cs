using System;
using System.Collections.Generic;

namespace RLEngine.Entities.NPCs
{
	[Serializable]
	public class MonsterSaveDetails
	{
		public int CurrentMaxId;
		public List<int> FreeActorIds;
		public SortedDictionary<int, Monster> CurrentMonsters;
	}
}

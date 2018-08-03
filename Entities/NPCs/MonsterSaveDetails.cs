// Tidied for version 0.3.

using System;
using System.Collections.Generic;

namespace TMNS.Entities.Monsters
{
	[Serializable]
	public class MonsterSaveDetails
	{
		public int CurrentMaxId;
		public List<int> FreeActorIds;
		public SortedDictionary<int, Monster> CurrentMonsters;
	}
}

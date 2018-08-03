using System;
using System.Collections.Generic;
using TMNS.Levels;
using TMNS.Entities.Monsters;
using TMNS.Entities.Furnishings;
using TMNS.Entities.Player;
using TMNS.Quests;
using TMNS.Resources.RNG;

namespace TMNS.UserData
{
	[Serializable]
	public class SaveGame
	{
		public SaveGameSummary Summary;
		public Dictionary<LevelId, LevelSaveSummary> Levels;
		public MonsterSaveDetails Monsters;
		public FurnishingSaveDetails Furnishings;

		public RandomNumberSaveData TopLevelRNG;
		public RandomNumberSaveData CombatRNG;
		public RandomNumberSaveData LootRNG;
		public RandomNumberSaveData MiscRNG;

		// TODO: Add the current state of any random number generation when ready.

		public StatisticsManager CurrentStatistics;
		// TODO: Add achievements and quests here too when ready.

		public int CurrentTime;
		public Player Player;
		public LevelId CurrentLevelId;

		public SaveGame(SaveGameSummary summary)
		{
			Summary = summary;
			Levels = new Dictionary<LevelId, LevelSaveSummary>();
		}
	}
}

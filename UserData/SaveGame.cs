using System;
using System.Collections.Generic;
using RLEngine.Levels;
using RLEngine.Entities.Monsters;
using RLEngine.Entities.Furnishings;
using RLEngine.Entities.Player;
using RLEngine.Quests;
using RLEngine.Resources.RNG;

namespace RLEngine.UserData
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

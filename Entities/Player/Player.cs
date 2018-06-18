using System;
using System.Collections.Generic;
using RLEngine.Entities.Actors;

namespace RLEngine.Entities.Player
{
	[Serializable]
	public class Player:Actor
	{
		static Player player;

		public Player()
			:base("Player", 0, 0, new Dictionary<string, string>())
		{
			player = this;

			Symbol = '@';
		}

		protected override void GetNextMove(Levels.Level currentLevel)
		{
			
		}

		public static Player GetPlayer()
		{
			return player;
		}

		public static void UpdatePlayer(Levels.Level currentLevel)
		{
			player.Update(currentLevel);
		}
	}
}

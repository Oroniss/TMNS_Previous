using System;
using System.Collections.Generic;
using RLEngine.Entities.Actors;

namespace RLEngine.Entities.Player
{
	[Serializable]
	public class Player:Actor
	{
		static Player player;

		public Player(UserData.GameData newGameData)
			:base("Player", 0, 0, new Dictionary<string, string>())
		{
			player = this;

			Symbol = '@';
		}

		protected override void GetNextMove(Levels.Level currentLevel)
		{
			bool NeedsToMove = true;

			while (NeedsToMove)
			{
				var key = UserInterface.UserInputHandler.GetNextKey();

				if (UserInterface.UserInputHandler.DirectionKeys.ContainsKey(key))
				{
					var direction = UserInterface.UserInputHandler.DirectionKeys[key];

					var result = currentLevel.MoveActorAttempt(this, direction.X, direction.Y);

					if (result)
						NeedsToMove = false;
				}

				if (key == "ESCAPE")
				{
					NeedsToMove = false;
					MainProgram.Quit();
				}
			}
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

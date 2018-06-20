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

			this.AddTrait(Trait.Player);
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

					var result = MakeMoveAttempt(currentLevel, direction.X, direction.Y);

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

		protected override bool MakeMoveAttempt(Levels.Level currentLevel, int deltaX, int deltaY)
		{
			if (HasTrait(Trait.Immobilised))
			{
				MainGraphicDisplay.TextConsole.AddOutputText("You can't move right now");
				return false;
			}

			int newX = deltaX + _xLoc;
			int newY = deltaY + _yLoc;

			if (!currentLevel.IsValidMapCoord(newX, newY))
			{
				MainGraphicDisplay.TextConsole.AddOutputText("Stay within the map");
				return false;
			}

			if (!currentLevel.IsPassible(this, newX, newY))
			{
				MainGraphicDisplay.TextConsole.AddOutputText("You can't move there");
				return false;
			}

			return base.MakeMoveAttempt(currentLevel, deltaX, deltaY);
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

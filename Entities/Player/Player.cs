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
			:base(new EntityBasicDetails("Player", '@', "Black", new List<Trait> {Trait.Player }), 0, 0, new Dictionary<string, string>())
		{
			// TODO: Flesh this out properly and tie it into regular Actor constructor.
			player = this;
		}

		protected override void GetNextMove(Levels.Level currentLevel)
		{
			var inSight = currentLevel.GetFOV(XLoc, YLoc, ViewDistance);
			currentLevel.VisibleTiles = inSight;
			foreach (Resources.Geometry.XYCoordinateStruct tile in inSight)
				currentLevel.RevealTile(tile.X, tile.Y);
			SpotHidden(currentLevel, currentLevel.GetConcealedEntity(inSight));
			MainGraphicDisplay.UpdateGameScreen();

			bool hasNotMoved = true;

			while (hasNotMoved)
			{
				var key = UserInterface.UserInputHandler.GetNextKey();

				if (UserInterface.UserInputHandler.DirectionKeys.ContainsKey(key))
				{
					var direction = UserInterface.UserInputHandler.DirectionKeys[key];

					var result = MakeMoveAttempt(currentLevel, direction.X, direction.Y);

					if (result)
						hasNotMoved = false;
				}

				if (key == "U")
				{
					var direction = UserInterface.UserInputHandler.GetDirection("Which direction", true);
					if (direction == null)
						continue;
					if (currentLevel.HasFurnishing(XLoc + direction.X, YLoc + direction.Y))
					{
						var furnishing = currentLevel.GetFurnishing(XLoc + direction.X, YLoc + direction.Y);
						furnishing.InteractWith(this);
					}
					else
					{
						MainGraphicDisplay.TextConsole.AddOutputText("There is nothing there to make use of");
					}
					hasNotMoved = false;
				}

				if (key == "ESCAPE")
				{
					hasNotMoved = false;
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

		void SpotHidden(Levels.Level currentLevel, List<Entity> concealedEntities)
		{
			foreach (Entity entity in concealedEntities)
			{
				var concealmentLevel = int.Parse(entity.GetOtherAttributeValue("ConcealmentLevel"));
				if (currentLevel.InSight(XLoc, YLoc, entity.XLoc, entity.YLoc, ViewDistance) &&
					Resources.Geometry.DistanceFunctions.Distance(this, entity) <= 5 - concealmentLevel)
				{
					entity.PlayerSpotted = true;
					entity.SetOtherAttribute("ConcealmentLevel", null);
					// TODO: Add some text here - actually make it an event and a statistic.
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

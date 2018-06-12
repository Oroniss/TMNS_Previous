using System;

namespace RLEngine.UserData
{
	[Serializable]
	public class SaveGameSummary
	{
		public GameData GameData;
		public string CurrentLevelName;
		public bool StillAlive;

		public SaveGameSummary(GameData gameData, string currentLevelName, bool stillAlive)
		{
			GameData = gameData;
			StillAlive = stillAlive;
			CurrentLevelName = currentLevelName;
		}

		public override string ToString()
		{
			var returnString = "Game Identifier: {0}, Current Level: {1}, Still Alive: {2}";
			return string.Format(returnString, GameData.GameIdentifier, CurrentLevelName, StillAlive);
		}
	}
}

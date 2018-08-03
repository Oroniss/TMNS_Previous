// Tidied for version 0.3.

using System;

namespace TMNS.UserData
{
	[Serializable]
	public class SaveGameSummary
	{
		public GameData GameData;
		public string CurrentLevelName;

		public SaveGameSummary(GameData gameData, string currentLevelName)
		{
			GameData = gameData;
			CurrentLevelName = currentLevelName;
		}

		public override string ToString()
		{
			var returnString = "Game Identifier: {0}, Current Level: {1}";
			return string.Format(returnString, GameData.GameIdentifier, CurrentLevelName);
		}
	}
}

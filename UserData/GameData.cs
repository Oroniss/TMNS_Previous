using System;

namespace RLEngine.UserData
{
	[Serializable]
	public class GameData
	{
		public int GameID;
		public string GameIdentifier;

		public GameData()
		{
		}

		public GameData(int gameId, string identifier)
		{
			GameID = gameId;
			GameIdentifier = identifier;
		}
	}
}

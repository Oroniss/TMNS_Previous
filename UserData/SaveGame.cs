// Finished up for version 0.1 - no change for 0.2.

using System;

namespace RLEngine.UserData
{
	[Serializable]
	public class SaveGame
	{
		public SaveGameSummary Summary;

		public SaveGame(SaveGameSummary summary)
		{
			Summary = summary;
		}
	}
}

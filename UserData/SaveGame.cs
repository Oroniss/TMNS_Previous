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
		/*
		public LevelSerialisationDetails CurrentLevelDetails;
		public int CurrentTime;
		public Entities.FurnishingSave FurnishingDetails;
		public Entities.HarvestingNodeSave HarvestingNodeDetails;
		public Entities.NPCSave NPCDetails;
		public Entities.PlayerSave PlayerDetails;

		public SaveGame(SaveGameSummary summary, LevelSerialisationDetails currentLevelDetails, 
		                int currentTime,
						Entities.FurnishingSave furnishingDetails,
		                Entities.HarvestingNodeSave harvestingNodeDetails,
		               	Entities.NPCSave npcDetails,
		                Entities.PlayerSave playerDetails)
		{
			CurrentLevelDetails = currentLevelDetails;
			CurrentTime = currentTime;

			FurnishingDetails = furnishingDetails;
			HarvestingNodeDetails = harvestingNodeDetails;
			NPCDetails = npcDetails;
			PlayerDetails = playerDetails;
		}
		*/
	}
}

// Tidied up for version 0.3.

using System.Collections.Generic;

namespace RLEngine.Entities
{
	public partial class Entity
	{
		static void SetupExtraParameters(Entity entity, Dictionary<string, string> otherParameters)
		{
			if (otherParameters.ContainsKey("Concealed"))
                SetupConcealedEntity(entity, otherParameters);
		}

		static void SetupConcealedEntity(Entity entity, Dictionary<string, string> otherParameters)
		{
			if (otherParameters.ContainsKey("Concealed") && otherParameters.ContainsKey("ConcealmentLevel"))
			{
				entity.PlayerSpotted = false;
				entity.Concealed = true;
				entity.SetOtherAttribute("ConcealmentLevel", otherParameters["ConcealmentLevel"]);
			}
			else
				ErrorLogger.AddDebugText(string.Format("Incorrect Concealment Specification at: {0}, {1}",
													   entity.XLoc, entity.YLoc));	
		}
	}
}

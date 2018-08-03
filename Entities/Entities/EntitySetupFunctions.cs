using System.Collections.Generic;

namespace TMNS.Entities
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
			if (otherParameters.ContainsKey("Concealed") && otherParameters.ContainsKey("SpotDC"))
			{
				entity.PlayerSpotted = false;
				entity.Concealed = true;
				entity._spotDC = int.Parse(otherParameters["SpotDC"]);
			}
			else
				ErrorLogger.AddDebugText(string.Format("Incorrect Concealment Specification at: {0}, {1}",
													   entity.XLoc, entity.YLoc));	
		}
	}
}

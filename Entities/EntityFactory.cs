using System.Collections.Generic;
using RLEngine.Entities.Furnishings;

namespace RLEngine.Entities
{
	public static class EntityFactory
	{
		static Dictionary<string, FurnishingDetails> furnishings = new Dictionary<string, FurnishingDetails>();

		public static Furnishing CreateFurnishing(string furnishingName, int xLoc, int yLoc, Dictionary<string, string>
		                                         otherParameters)
		{
			if (!furnishings.ContainsKey(furnishingName))
				furnishings[furnishingName] = StaticDatabase.StaticDatabaseConnection.GetFurnishingDetails(furnishingName);

			return new Furnishing(furnishings[furnishingName], xLoc, yLoc, otherParameters);
		}
	}
}

using System.Collections.Generic;
using RLEngine.Entities.Furnishings;

namespace RLEngine.Entities
{
	public static class EntityFactory
	{
		public static Furnishing CreateFurnishing(string furnishingName, int xLoc, int yLoc, Dictionary<string, string>
		                                         otherParameters)
		{
			if (furnishingName == "TestFurnishing1")
			{
				var details = new FurnishingDetails("TestFurnishing1", '#', "Red", new Trait[] { Trait.BlockMove, Trait.BlockLOS });
				return new Furnishing(details, xLoc, yLoc, new Dictionary<string, string>());
			}
			else if (furnishingName == "TestFurnishing2")
			{
				var details = new FurnishingDetails("TestFurnishing2", '*', "Olive", new Trait[] { Trait.TestTrait1, Trait.TestTrait2 });
				return new Furnishing(details, xLoc, yLoc, new Dictionary<string, string>());
			}
			return null;
		}
	}
}

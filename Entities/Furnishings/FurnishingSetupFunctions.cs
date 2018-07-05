using System.Collections.Generic;

namespace RLEngine.Entities.Furnishings
{
	public partial class Furnishing
	{
		static readonly Dictionary<string, SetupFunction> furnishingSetupFunctions = new Dictionary<string, SetupFunction>
		{
			
		};

		delegate void SetupFunction(Furnishing entity, Dictionary<string, string> otherParameters);

		static void DefaultFurnishingSetup(Furnishing entity, Dictionary<string, string> otherParameters)
		{
			entity._moveOnFunction = "";
			entity._moveOffFunction = "";
			entity._interactionFunction = "No Use";
		}
	}
}

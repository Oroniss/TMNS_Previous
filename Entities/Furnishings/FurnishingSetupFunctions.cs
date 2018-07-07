using System.Collections.Generic;

namespace RLEngine.Entities.Furnishings
{
	public partial class Furnishing
	{
		static readonly Dictionary<string, SetupFunction> furnishingSetupFunctions = new Dictionary<string, SetupFunction>
		{
			{"TestFurnishing1", TestFurnishingSetup1}
		};

		delegate void SetupFunction(Furnishing furnishing, Dictionary<string, string> otherParameters);

		static void DefaultFurnishingSetup(Furnishing furnishing, Dictionary<string, string> otherParameters)
		{
			furnishing._moveOnFunction = "";
			furnishing._moveOffFunction = "";
			furnishing._interactionFunction = "No Use";
		}

		static void TestFurnishingSetup1(Furnishing furnishing, Dictionary<string, string> otherParameters)
		{
			furnishing._moveOnFunction = "";
			furnishing._moveOffFunction = "";
			furnishing._interactionFunction = "TestInteractionFunction1";
		}
	}
}

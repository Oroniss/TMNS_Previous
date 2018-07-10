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

		static void SetupExtraParameers(Furnishing furnishing, Dictionary<string, string> otherParameters)
		{
			System.Console.WriteLine(otherParameters.Count);
			if (otherParameters.ContainsKey("LevelTransition"))
				SetupLevelTransitionObject(furnishing, otherParameters);
		}

		// General setup functions or "Add-ons"
		static void SetupLevelTransitionObject(Furnishing furnishing, Dictionary<string, string> otherParameters)
		{
			if (otherParameters.ContainsKey("DestinationLevel") && otherParameters.ContainsKey("DestinationXLoc") &&
				otherParameters.ContainsKey("DestinationYLoc"))
			{
				furnishing.SetOtherAttribute("DestinationLevel", otherParameters["DestinationLevel"]);
				furnishing.SetOtherAttribute("DestinationXLoc", otherParameters["DestinationXLoc"]);
				furnishing.SetOtherAttribute("DestinationYLoc", otherParameters["DestinationYLoc"]);
				furnishing.InteractionFunctionName = "LevelTransitionInteraction";
			}
			else
				ErrorLogger.AddDebugText(string.Format("Incorrect Level Transition Object Specification at: {0}, {1}",
													   furnishing.XLoc, furnishing.YLoc));
		}

		// Specific Furnishing Setup Functions
		static void TestFurnishingSetup1(Furnishing furnishing, Dictionary<string, string> otherParameters)
		{
			furnishing._interactionFunction = "TestInteractionFunction1";
		}
	}
}

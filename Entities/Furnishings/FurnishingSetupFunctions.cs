using System.Collections.Generic;

namespace TMNS.Entities.Furnishings
{
	public partial class Furnishing
	{
		static readonly Dictionary<string, SetupFunction> furnishingSetupFunctions = new Dictionary<string, SetupFunction>
		{
		};

		delegate void SetupFunction(Furnishing furnishing, Dictionary<string, string> otherParameters);

		static void SetupExtraParameters(Furnishing furnishing, Dictionary<string, string> otherParameters)
		{
			if (otherParameters.ContainsKey("LevelTransition"))
				SetupLevelTransitionObject(furnishing, otherParameters);
			if (otherParameters.ContainsKey("InteractionTrap"))
				SetupInteractionTrap(furnishing, otherParameters);
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

		static void SetupInteractionTrap(Furnishing furnishing, Dictionary<string, string> otherParameters)
		{
			if (otherParameters.ContainsKey("TrapType") && otherParameters.ContainsKey("TrapLevel"))
			{
				furnishing.SetOtherAttribute("TrapType", otherParameters["TrapType"]);
				furnishing.SetOtherAttribute("TrapLevel", otherParameters["TrapLevel"]);
				furnishing.InteractionTrapName = otherParameters["TrapType"] + "Interaction";
			}
			else
				ErrorLogger.AddDebugText(string.Format("Incorrect Interaction Trap Specification at: {0}, {1}",
													   furnishing.XLoc, furnishing.YLoc));
		}

		// Specific Furnishing Setup Functions - maybe think about putting the auto ones into a db?
	}
}

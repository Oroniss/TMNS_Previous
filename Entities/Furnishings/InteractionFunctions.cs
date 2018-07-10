using System.Collections.Generic;
using RLEngine.Entities.Actors;
using System;

namespace RLEngine.Entities.Furnishings
{
	public partial class Furnishing
	{
		static Dictionary<string, InteractionFunction> interactionFunctions = new Dictionary<string, InteractionFunction>
		{
			{"TestInteractionFunction1", TestInteractionFunction1},
			{"LevelTransitionInteraction", LevelTransitionInteraction},

			// Interaction Traps
			{"InteractionTrap1Interaction", InteractionTrap1Interaction},
			{"InteractionTrap2Interaction", InteractionTrap2Interaction},
		};

		delegate void InteractionFunction(Furnishing furnishing, Actor actor);

		static void TestInteractionFunction1(Furnishing furnishing, Actor actor)
		{
			if (furnishing.HasTrait(Trait.BlockMove))
				furnishing.RemoveTrait(Trait.BlockMove);
			else
				furnishing.AddTrait(Trait.BlockMove);
		}

		static void LevelTransitionInteraction(Furnishing furnishing, Actor actor)
		{
			if (actor.HasTrait(Trait.Player))
			{
				var destinationLevel = (Levels.LevelId)Enum.Parse(typeof(Levels.LevelId),
												furnishing.GetOtherAttributeValue("DestinationLevel"));
				var destinationXLoc = int.Parse(furnishing.GetOtherAttributeValue("DestinationXLoc"));
				var destinationYLoc = int.Parse(furnishing.GetOtherAttributeValue("DestinationYLoc"));

				MainProgram.LevelTransition(destinationLevel, destinationXLoc, destinationYLoc);
			}
		}

		static void InteractionTrap1Interaction(Furnishing furnishing, Actor actor)
		{
			var trapLevel = int.Parse(furnishing.GetOtherAttributeValue("TrapLevel"));
			if (actor.HasTrait(Trait.Player))
				MainGraphicDisplay.TextConsole.AddOutputText(string.Format("Trap hits you for {0} damage", trapLevel));
		}

		static void InteractionTrap2Interaction(Furnishing furnishing, Actor actor)
		{
			var trapLevel = int.Parse(furnishing.GetOtherAttributeValue("TrapLevel"));
			if (actor.HasTrait(Trait.Player))
				MainGraphicDisplay.TextConsole.AddOutputText(string.Format("Trap hits you for {0} damage", trapLevel));
			furnishing.InteractionTrapName = null;
			furnishing.SetOtherAttribute("TrapType", null);
			furnishing.SetOtherAttribute("TrapLevel", null);
		}
	}
}

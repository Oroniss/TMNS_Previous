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
	}
}

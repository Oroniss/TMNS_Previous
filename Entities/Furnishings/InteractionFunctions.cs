using System.Collections.Generic;
using RLEngine.Entities.Actors;

namespace RLEngine.Entities.Furnishings
{
	public partial class Furnishing
	{
		static Dictionary<string, InteractionFunction> interactionFunctions = new Dictionary<string, InteractionFunction>
		{
			{"TestInteractionFunction1", TestInteractionFunction1}
		};

		delegate void InteractionFunction(Furnishing furnishing, Actor actor);

		static void TestInteractionFunction1(Furnishing furnishing, Actor actor)
		{
			if (furnishing.HasTrait(Trait.BlockMove))
				furnishing.RemoveTrait(Trait.BlockMove);
			else
				furnishing.AddTrait(Trait.BlockMove);
		}
	}
}

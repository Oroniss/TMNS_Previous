using System;
using System.Collections.Generic;

namespace TMNS.Entities.Actors
{
	[Serializable]
	public class ActorDetails:EntityBasicDetails
	{
		public ActorDetails(string entityName, char symbol, string fgColorName, List<Trait> traits)
			:base(entityName, symbol, fgColorName, traits){ }

		public ActorDetails(string entityName, char symbol, string fgColorName, Trait[] traits)
			:base(entityName, symbol, fgColorName, traits){ }
	}
}

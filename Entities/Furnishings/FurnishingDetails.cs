using System;
using System.Collections.Generic;

namespace RLEngine.Entities.Furnishings
{
	[Serializable]
	public class FurnishingDetails:EntityBasicDetails
	{
		public FurnishingDetails(string entityName, char symbol, string fgColorName, List<Trait> traits)
			: base(entityName, symbol, fgColorName, traits) { }

		public FurnishingDetails(string entityName, char symbol, string fgColorname, Trait[] traits)
			: base(entityName, symbol, fgColorname, traits) { }
	}
}

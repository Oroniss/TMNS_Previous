using System;
using System.Collections.Generic;

namespace TMNS.Entities.Furnishings
{
	[Serializable]
	public class FurnishingDetails:EntityBasicDetails
	{
		public string Description;
		public Material Material;

		public FurnishingDetails(string entityName, char symbol, string fgColorName, string description,
		                         Material material, List<Trait> traits)
			: base(entityName, symbol, fgColorName, traits) 
		{
			Description = description;
			Material = material;
		}

		public FurnishingDetails(string entityName, char symbol, string fgColorname, string description,
		                         Material material, Trait[] traits)
			: base(entityName, symbol, fgColorname, traits) 
		{
			Description = description;
			Material = material;
		}
	}
}

using System;
using System.Collections.Generic;

namespace RLEngine.Entities.Furnishings
{
	[Serializable]
	public partial class Furnishing:Entity
	{
		static int currentMaxId = 1;
		static List<int> freeFurnishingIds = new List<int>();

		static readonly List<Trait> furnishingTraits = new List<Trait> {Trait.ImmuneToPoison };

		int _furnishingId;
		bool _trapped;

		string _moveOnFunction;
		string _moveOffFunction;
		string _interactionFunction;

		public Furnishing(FurnishingDetails details, int xLoc, int yLoc, Dictionary<string, string> otherParameters)
			: base(details, xLoc, yLoc, otherParameters)
		{
			if (freeFurnishingIds.Count > 0)
			{
				_furnishingId = freeFurnishingIds[0];
				freeFurnishingIds.RemoveAt(0);
			}
			else
			{
				_furnishingId = currentMaxId;
				currentMaxId++;
			}

			// Sets up all the functions in here.
			if (furnishingSetupFunctions.ContainsKey(EntityName))
				furnishingSetupFunctions[EntityName](this, otherParameters);
			else
				DefaultFurnishingSetup(this, otherParameters);
		}

		public int FurnishingId
		{
			get { return _furnishingId; }
		}
		
	}
}

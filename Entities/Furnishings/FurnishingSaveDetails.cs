using System;
using System.Collections.Generic;

namespace RLEngine.Entities.Furnishings
{
	[Serializable]
	public class FurnishingSaveDetails
	{
		public int CurrentMaxId;
		public List<int> FreeFurnishingIds;
		public SortedDictionary<int, Furnishing> CurrentFurnishings;
	}
}

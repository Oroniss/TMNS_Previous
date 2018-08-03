using System;
using System.Collections.Generic;

namespace TMNS.Entities.Furnishings
{
	[Serializable]
	public class FurnishingSaveDetails
	{
		public int CurrentMaxId;
		public List<int> FreeFurnishingIds;
		public SortedDictionary<int, Furnishing> CurrentFurnishings;
	}
}

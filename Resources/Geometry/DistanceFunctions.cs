using System;
using RLEngine.Entities;

namespace RLEngine.Resources.Geometry
{
	public static class DistanceFunctions
	{
		public static int Distance(int x1, int y1, int x2, int y2)
		{
			return (int)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
		}

		public static int Distance(Entity entity1, Entity entity2)
		{
			return Distance(entity1.XLoc, entity1.YLoc, entity2.XLoc, entity2.YLoc);
		}

		// TODO: Add the others here at some point.
	}
}

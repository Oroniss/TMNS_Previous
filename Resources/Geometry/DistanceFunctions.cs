using System;
using TMNS.Entities;

namespace TMNS.Resources.Geometry
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

		public static int DistanceSquared(int x1, int y1, int x2, int y2)
		{
			return (int)(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
		}

		public static int DistanceSquared(Entity entity1, Entity entity2)
		{
			return DistanceSquared(entity1.XLoc, entity1.YLoc, entity2.XLoc, entity2.YLoc);
		}

		public static int ManhattanDistance(int x1, int y1, int x2, int y2)
		{
			return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
		}

		public static int ManhattanDistance(Entity entity1, Entity entity2)
		{
			return ManhattanDistance(entity1.XLoc, entity1.YLoc, entity2.XLoc, entity2.YLoc);
		}
	}
}

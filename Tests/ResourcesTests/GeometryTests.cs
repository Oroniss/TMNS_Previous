using NUnit.Framework;
using TMNS.Resources.Geometry;

using TMNS.Entities.Furnishings;
using System.Collections.Generic;

namespace TMNS.Tests
{
	[TestFixture]
	public class GeometryTests
	{
		[Test]
		public void TestXYCoordinateClass()
		{
			var xyClass = new XYCoordinateClass(5, 10);

			Assert.AreEqual(5, xyClass.X);
			Assert.AreEqual(10, xyClass.Y);

			xyClass.X = 12;
			Assert.AreEqual(12, xyClass.X);

			xyClass.Y = 15;
			Assert.AreEqual(15, xyClass.Y);
		}

		[Test]
		public void TestXYCoordinateStruct()
		{
			var xyStruct = new XYCoordinateStruct(5, 10);

			Assert.AreEqual(5, xyStruct.X);
			Assert.AreEqual(10, xyStruct.Y);
		}

		[Test]
		public void TestDistance()
		{
			Assert.AreEqual(5, DistanceFunctions.Distance(0, 0, 3, 4));
			Assert.AreEqual(9, DistanceFunctions.Distance(4, 0, 10, 7));
			Assert.AreEqual(15, DistanceFunctions.Distance(10, 20, 20, 32));

			var details = new FurnishingDetails("Test", ' ', "Test", "", Entities.Material.Immune, new List<Entities.Trait>());
			var e1 = new Furnishing(details, 0, 0, new Dictionary<string, string>());
			var e2 = new Furnishing(details, 4, 3, new Dictionary<string, string>());
			Assert.AreEqual(5, DistanceFunctions.Distance(e1, e2));

			e1 = new Furnishing(details, 4, 0, new Dictionary<string, string>());
			e2 = new Furnishing(details, 10, 7, new Dictionary<string, string>());
			Assert.AreEqual(9, DistanceFunctions.Distance(e1, e2));

			e1 = new Furnishing(details, 10, 20, new Dictionary<string, string>());
			e2 = new Furnishing(details, 20, 32, new Dictionary<string, string>());
			Assert.AreEqual(15, DistanceFunctions.Distance(e1, e2));

			e1 = new Furnishing(details, 9, 7, new Dictionary<string, string>());
			e2 = new Furnishing(details, 9, 7, new Dictionary<string, string>());
			Assert.AreEqual(0, DistanceFunctions.Distance(e1, e2));
		}

		[Test]
		public void TestDistanceSquared()
		{
			Assert.AreEqual(25, DistanceFunctions.DistanceSquared(0, 0, 3, 4));
			Assert.AreEqual(85, DistanceFunctions.DistanceSquared(4, 0, 10, 7));
			Assert.AreEqual(244, DistanceFunctions.DistanceSquared(10, 20, 20, 32));

			var details = new FurnishingDetails("Test", ' ', "Test", "", Entities.Material.Immune, new List<Entities.Trait>());
			var e1 = new Furnishing(details, 0, 0, new Dictionary<string, string>());
			var e2 = new Furnishing(details, 4, 3, new Dictionary<string, string>());
			Assert.AreEqual(25, DistanceFunctions.DistanceSquared(e1, e2));

			e1 = new Furnishing(details, 4, 0, new Dictionary<string, string>());
			e2 = new Furnishing(details, 10, 7, new Dictionary<string, string>());
			Assert.AreEqual(85, DistanceFunctions.DistanceSquared(e1, e2));

			e1 = new Furnishing(details, 10, 20, new Dictionary<string, string>());
			e2 = new Furnishing(details, 20, 32, new Dictionary<string, string>());
			Assert.AreEqual(244, DistanceFunctions.DistanceSquared(e1, e2));

			e1 = new Furnishing(details, 9, 7, new Dictionary<string, string>());
			e2 = new Furnishing(details, 9, 7, new Dictionary<string, string>());
			Assert.AreEqual(0, DistanceFunctions.DistanceSquared(e1, e2));
		}

		[Test]
		public void TestManhattanDistance()
		{
			Assert.AreEqual(7, DistanceFunctions.ManhattanDistance(0, 0, 3, 4));
			Assert.AreEqual(13, DistanceFunctions.ManhattanDistance(4, 0, 10, 7));
			Assert.AreEqual(22, DistanceFunctions.ManhattanDistance(10, 20, 20, 32));

			var details = new FurnishingDetails("Test", ' ', "Test", "", Entities.Material.Immune, new List<Entities.Trait>());
			var e1 = new Furnishing(details, 0, 0, new Dictionary<string, string>());
			var e2 = new Furnishing(details, 4, 3, new Dictionary<string, string>());
			Assert.AreEqual(7, DistanceFunctions.ManhattanDistance(e1, e2));

			e1 = new Furnishing(details, 4, 0, new Dictionary<string, string>());
			e2 = new Furnishing(details, 10, 7, new Dictionary<string, string>());
			Assert.AreEqual(13, DistanceFunctions.ManhattanDistance(e1, e2));

			e1 = new Furnishing(details, 10, 20, new Dictionary<string, string>());
			e2 = new Furnishing(details, 20, 32, new Dictionary<string, string>());
			Assert.AreEqual(22, DistanceFunctions.ManhattanDistance(e1, e2));

			e1 = new Furnishing(details, 9, 7, new Dictionary<string, string>());
			e2 = new Furnishing(details, 9, 7, new Dictionary<string, string>());
			Assert.AreEqual(0, DistanceFunctions.ManhattanDistance(e1, e2));
		}
	}
}

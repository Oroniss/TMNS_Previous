// Finished for version 0.1.

using NUnit.Framework;
using RLEngine.Resources.Geometry;

namespace RLEngine.Tests
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
	}
}

using NUnit.Framework;
using TMNS.Resources.Palette;

namespace TMNS.Tests
{
	[TestFixture]
	public class PaletteTests
	{
		[Test]
		public void TestGetColor()
		{
			ErrorLogger.SetToTest();

			var red = Palette.GetColor("Red");
			Assert.AreEqual((float)255/255, red.r);
			Assert.AreEqual(0, red.g);
			Assert.AreEqual(0, red.b);

			var blue = Palette.GetColor("Blueee");
			Assert.AreEqual(0, blue.b);
			Assert.AreEqual(0, blue.r);
			Assert.AreEqual(0, blue.g);
			Assert.AreEqual("Unknown color name: Blueee", ErrorLogger.GetNextTestMessage());
		}
	}
}

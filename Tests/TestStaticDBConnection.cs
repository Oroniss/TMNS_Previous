// Tidied for version 0.3.

using NUnit.Framework;
using RLEngine.StaticDatabase;

namespace RLEngine.Tests
{
	[TestFixture]
	public class TestStaticDBConnection
	{
		static string defaultDebugMessage = "No Debug Messages";
		static string databasePath = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "UnitTestFiles");

		[SetUp]
		public void Setup()
		{
			ErrorLogger.SetToTest();
			StaticDatabaseConnection.SetToTest(databasePath);
			StaticDatabaseConnection.OpenDBConnection();
		}

		[TearDown]
		public void TearDown()
		{
			StaticDatabaseConnection.CloseDBConnection();		
		}

		[Test]
		public void TestGetMapTileDetails()
		{
			var mapTileDetails = StaticDatabaseConnection.GetMapTileDetails("TestTile2");

			Assert.AreEqual("LightBlue", mapTileDetails.BackgroundColor);
			Assert.AreEqual("Blue", mapTileDetails.FogColor);
		}

		[Test]
		public void TestGetFurnishing()
		{
			var furnishing = StaticDatabaseConnection.GetFurnishingDetails("TestFurnishing2");

			Assert.AreEqual('*', furnishing.Symbol);
			Assert.IsTrue(furnishing.Traits.Contains(Entities.Trait.TestTrait1));
		}

		[Test]
		public void TestErrorHandling()
		{
			// MapTiles
			ErrorLogger.ClearTestMessages();
			var tile1 = StaticDatabaseConnection.GetMapTileDetails(Entities.MapTiles.TileType.TestTile3);
			Assert.AreEqual("Unknown map tile type: TestTile3", ErrorLogger.GetNextTestMessage());

			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			Assert.AreEqual("GraySeven", tile1.BackgroundColor);
			Assert.AreEqual("GrayFour", tile1.FogColor);

			// Furnishings
			ErrorLogger.ClearTestMessages();
			var furnishingDetails = StaticDatabaseConnection.GetFurnishingDetails("Not A Furnishing");
			Assert.AreEqual("Unknown furnishing: Not A Furnishing", ErrorLogger.GetNextTestMessage());

			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			Assert.AreEqual('#', furnishingDetails.Symbol);
			Assert.AreEqual("Red", furnishingDetails.FGColorName);
		}
	}
}

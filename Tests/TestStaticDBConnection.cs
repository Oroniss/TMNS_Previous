using NUnit.Framework;
using TMNS.StaticDatabase;

namespace TMNS.Tests
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
			var mapTileDetails = StaticDatabaseConnection.GetMapTileDetails("IcyLedge");

			Assert.AreEqual("SteelBlueThree", mapTileDetails.BackgroundColor);
			Assert.AreEqual("SteelBlueThree", mapTileDetails.FogColor);
		}

		[Test]
		public void TestGetFurnishing()
		{
			var furnishing = StaticDatabaseConnection.GetFurnishingDetails("IcyPillar");

			Assert.AreEqual('*', furnishing.Symbol);
			Assert.IsTrue(furnishing.Traits.Contains(Entities.Trait.BlockMove));
		}

		[Test]
		public void TestErrorHandling()
		{
			// MapTiles
			ErrorLogger.ClearTestMessages();
			var tile1 = StaticDatabaseConnection.GetMapTileDetails(Entities.MapTiles.TileType.Grease);
			Assert.AreEqual("Unknown map tile type: Grease", ErrorLogger.GetNextTestMessage());

			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			Assert.AreEqual("Silver", tile1.BackgroundColor);
			Assert.AreEqual("GraySix", tile1.FogColor);

			// Furnishings
			ErrorLogger.ClearTestMessages();
			var furnishingDetails = StaticDatabaseConnection.GetFurnishingDetails("Not A Furnishing");
			Assert.AreEqual("Unknown furnishing: Not A Furnishing", ErrorLogger.GetNextTestMessage());

			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			Assert.AreEqual('.', furnishingDetails.Symbol);
			Assert.AreEqual("DarkViolet", furnishingDetails.FGColorName);
		}
	}
}

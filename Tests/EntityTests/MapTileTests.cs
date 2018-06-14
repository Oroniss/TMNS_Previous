// Finished for version 0.1.

using NUnit.Framework;
using RLEngine.Entities.MapTiles;

namespace RLEngine
{
	[TestFixture]
	public class MapTileTests
	{
		static string defaultDebugMessage = "No Debug Messages";

		[SetUp]
		public void Setup()
		{
			MapTileDetails.SetTestFilePath(TestContext.CurrentContext.TestDirectory);
			ErrorLogger.SetToTest();
		}

		[Test]
		public void TestMapTileDatabase()
		{
			MapTileDetails tile1 = MapTileDetails.GetTileDetails(TileType.TestTile1);
			Assert.AreEqual("GraySeven", tile1.BackgroundColor);
			Assert.AreEqual("GrayFour", tile1.FogColor);
			Assert.IsTrue(tile1.HasTrait(Entities.Trait.TestTrait2));
			Assert.IsFalse(tile1.HasTrait(Entities.Trait.TestTrait1));
			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			MapTileDetails tile2 = MapTileDetails.GetTileDetails(TileType.TestTile2);
			Assert.AreEqual("Blue", tile2.FogColor);
			Assert.AreEqual("LightBlue", tile2.BackgroundColor);
			Assert.IsTrue(tile2.HasTrait(Entities.Trait.TestTrait1));
			Assert.IsFalse(tile2.HasTrait(Entities.Trait.TestTrait2));
			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			// Test error message and default handling.
			ErrorLogger.ClearTestMessages();
			MapTileDetails tile3 = MapTileDetails.GetTileDetails(TileType.TestTile3);
			Assert.AreEqual("Couldn't find db entry for TileType TestTile3", ErrorLogger.GetNextTestMessage());
			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());
			Assert.AreEqual("GraySeven", tile3.BackgroundColor);
			Assert.AreEqual("GrayFour", tile3.FogColor);
		}

		[Test]
		public void TestMapTileDetails()
		{
			ErrorLogger.ClearTestMessages();
			MapTileDetails tile1 = MapTileDetails.GetTileDetails(TileType.TestTile1);
			tile1.AddTrait(Entities.Trait.TestTrait1);
			Assert.AreEqual("Tried to add trait TestTrait1 to map tile TestTile1", ErrorLogger.GetNextTestMessage());
			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			ErrorLogger.ClearTestMessages();
			tile1.RemoveTrait(Entities.Trait.TestTrait2);
			Assert.AreEqual("Tried to remove trait TestTrait2 from map tile TestTile1", ErrorLogger.GetNextTestMessage());
			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());
		}
	}
}

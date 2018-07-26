// Tidied up for version 0.3.

using NUnit.Framework;
using RLEngine.Entities.MapTiles;
using RLEngine.Entities;

namespace RLEngine.Tests
{
	[TestFixture]
	public class MapTileTests
	{
		static string defaultDebugMessage = "No Debug Messages";
		static string databasePath = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "UnitTestFiles");

		[SetUp]
		public void Setup()
		{
			ErrorLogger.SetToTest();
			StaticDatabase.StaticDatabaseConnection.SetToTest(databasePath);
			StaticDatabase.StaticDatabaseConnection.OpenDBConnection();
		}

		[TearDown]
		public void TearDown()
		{
			StaticDatabase.StaticDatabaseConnection.CloseDBConnection();
		}

		[Test]
		public void TestMapTileBasics()
		{
			var tile1 = EntityFactory.CreateMapTile(TileType.TestTile1);
			Assert.AreEqual("GraySeven", tile1.BackgroundColor);
			Assert.AreEqual("GrayFour", tile1.FogColor);
			Assert.IsTrue(tile1.HasTrait(Trait.TestTrait2));
			Assert.IsFalse(tile1.HasTrait(Trait.TestTrait1));

			var tile2 = EntityFactory.CreateMapTile(TileType.TestTile2);
			Assert.AreEqual("Blue", tile2.FogColor);
			Assert.AreEqual("LightBlue", tile2.BackgroundColor);
			Assert.IsTrue(tile2.HasTrait(Trait.TestTrait1));
			Assert.IsFalse(tile2.HasTrait(Trait.TestTrait2));
		}

		[Test]
		public void TestMapTileDetails()
		{
			ErrorLogger.ClearTestMessages();
			var tile1 = EntityFactory.CreateMapTile(TileType.TestTile1);
			tile1.AddTrait(Trait.TestTrait1);
			Assert.AreEqual("Tried to add trait TestTrait1 to map tile TestTile1", ErrorLogger.GetNextTestMessage());
			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			ErrorLogger.ClearTestMessages();
			tile1.RemoveTrait(Trait.TestTrait2);
			Assert.AreEqual("Tried to remove trait TestTrait2 from map tile TestTile1", ErrorLogger.GetNextTestMessage());
			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());
		}
	}
}

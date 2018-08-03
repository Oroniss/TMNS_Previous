using NUnit.Framework;
using TMNS.Entities.MapTiles;
using TMNS.Entities;

namespace TMNS.Tests
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
			var tile1 = EntityFactory.CreateMapTile(TileType.StoneFloor);
			Assert.AreEqual("Silver", tile1.BackgroundColor);
			Assert.AreEqual("GraySix", tile1.FogColor);
			Assert.AreEqual("", tile1.Description);
			Assert.IsFalse(tile1.HasTrait(Trait.TestTrait2));
			Assert.IsFalse(tile1.HasTrait(Trait.TestTrait1));

			var tile2 = EntityFactory.CreateMapTile(TileType.Wall);
			Assert.AreEqual("GrayTwo", tile2.FogColor);
			Assert.AreEqual("GrayTwo", tile2.BackgroundColor);
			Assert.AreEqual("a wall", tile2.Description);
			Assert.IsTrue(tile2.HasTrait(Trait.BlockLOS));
			Assert.IsTrue(tile2.HasTrait(Trait.BlockMove));

			// TODO: Add movement function tests too.
		}

		[Test]
		public void TestMapTileDetails()
		{
			ErrorLogger.ClearTestMessages();
			var tile1 = EntityFactory.CreateMapTile(TileType.StoneFloor);
			tile1.AddTrait(Trait.TestTrait1);
			Assert.AreEqual("Tried to add trait TestTrait1 to map tile StoneFloor", ErrorLogger.GetNextTestMessage());
			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			ErrorLogger.ClearTestMessages();
			tile1.RemoveTrait(Trait.TestTrait2);
			Assert.AreEqual("Tried to remove trait TestTrait2 from map tile StoneFloor", ErrorLogger.GetNextTestMessage());
			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());
		}
	}
}

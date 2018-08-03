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
			Assert.IsFalse(tile1.HasTrait(Trait.Fire));
			Assert.IsFalse(tile1.HasTrait(Trait.Water));

			var tile2 = EntityFactory.CreateMapTile(TileType.Wall);
			Assert.AreEqual("GrayTwo", tile2.FogColor);
			Assert.AreEqual("GrayTwo", tile2.BackgroundColor);
			Assert.AreEqual("a wall", tile2.Description);
			Assert.IsTrue(tile2.HasTrait(Trait.BlockLOS));
			Assert.IsTrue(tile2.HasTrait(Trait.BlockMove));

			// TODO: Add movement function tests too.
		}
	}
}

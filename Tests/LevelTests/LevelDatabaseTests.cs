// Fixed up for version 0.1 - no change for 0.2.

using NUnit.Framework;
using RLEngine.Levels.LevelDatabase;

namespace RLEngine.Tests
{
	[TestFixture]
	public class LevelDatabaseTests
	{
		[SetUp]
		public void Setup()
		{
			LevelDatabase.SetTestFilePath(TestContext.CurrentContext.TestDirectory);
			ErrorLogger.SetToTest();
		}

		[Test]
		public void TestGetLevelDetails()
		{
			var level1 = LevelDatabase.GetLevelTemplate(Levels.LevelId.TestLevel1);
			Assert.AreEqual("Small Test Level", level1.LevelName);
			Assert.AreEqual(Levels.LevelId.TestLevel1, level1.LevelId);
			Assert.AreEqual(8, level1.MapWidth);
			Assert.AreEqual(6, level1.MapHeight);
			Assert.AreEqual(Entities.MapTiles.TileType.TestTile1, level1.TileDictionary[0]);
			Assert.AreEqual(Entities.MapTiles.TileType.TestTile2, level1.TileDictionary[1]);
			Assert.AreEqual(0, level1.MapGrid[0]);
			Assert.AreEqual(0, level1.MapGrid[7]);
			Assert.AreEqual(1, level1.MapGrid[8]);
			Assert.AreEqual(0, level1.MapGrid[15]);
			Assert.AreEqual(1, level1.MapGrid[40]);
			Assert.AreEqual(1, level1.MapGrid[43]);
			Assert.AreEqual(0, level1.MapGrid[44]);
			Assert.AreEqual(48, level1.MapGrid.Length);

			var level2 = LevelDatabase.GetLevelTemplate(Levels.LevelId.TestLevel2);
			Assert.AreEqual("Large Test Level", level2.LevelName);
			Assert.AreEqual(Levels.LevelId.TestLevel2, level2.LevelId);
			Assert.AreEqual(60, level2.MapWidth);
			Assert.AreEqual(12, level2.MapHeight);
			Assert.AreEqual(Entities.MapTiles.TileType.TestTile1, level2.TileDictionary[0]);
			Assert.AreEqual(Entities.MapTiles.TileType.TestTile2, level2.TileDictionary[1]);
			Assert.AreEqual(0, level2.MapGrid[61]);
			Assert.AreEqual(1, level2.MapGrid[0]);
			Assert.AreEqual(0, level2.MapGrid[481]);
			Assert.AreEqual(1, level2.MapGrid[480]);
			Assert.AreEqual(720, level2.MapGrid.Length);
		}
		
	}
}

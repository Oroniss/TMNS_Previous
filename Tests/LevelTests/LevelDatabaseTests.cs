// Fixed up for version 0.3.

using System;
using NUnit.Framework;
using TMNS.Levels.LevelDatabase;
using TMNS.Levels;

namespace TMNS.Tests
{
	[TestFixture]
	public class LevelDatabaseTests
	{

		static string databasePath = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "UnitTestFiles");

		[SetUp]
		public void Setup()
		{
			LevelDatabase.SetTestFilePath(TestContext.CurrentContext.TestDirectory);
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
		public void TestGetLevelDetails()
		{
			var level1 = LevelDatabase.GetLevelTemplate(LevelId.TestLevel1);
			Assert.AreEqual("Small Test Level", level1.LevelName);
			Assert.AreEqual(LevelId.TestLevel1, level1.LevelId);
			Assert.AreEqual(8, level1.MapWidth);
			Assert.AreEqual(6, level1.MapHeight);
			Assert.AreEqual(Entities.MapTiles.TileType.StoneFloor, level1.TileDictionary[0]);
			Assert.AreEqual(Entities.MapTiles.TileType.Water, level1.TileDictionary[1]);
			Assert.AreEqual(0, level1.MapGrid[0]);
			Assert.AreEqual(0, level1.MapGrid[7]);
			Assert.AreEqual(1, level1.MapGrid[8]);
			Assert.AreEqual(0, level1.MapGrid[15]);
			Assert.AreEqual(1, level1.MapGrid[40]);
			Assert.AreEqual(1, level1.MapGrid[43]);
			Assert.AreEqual(0, level1.MapGrid[44]);
			Assert.AreEqual(48, level1.MapGrid.Length);

			var level2 = LevelDatabase.GetLevelTemplate(LevelId.TestLevel2);
			Assert.AreEqual("Large Test Level", level2.LevelName);
			Assert.AreEqual(LevelId.TestLevel2, level2.LevelId);
			Assert.AreEqual(60, level2.MapWidth);
			Assert.AreEqual(12, level2.MapHeight);
			Assert.AreEqual(Entities.MapTiles.TileType.StoneFloor, level2.TileDictionary[0]);
			Assert.AreEqual(Entities.MapTiles.TileType.Water, level2.TileDictionary[1]);
			Assert.AreEqual(0, level2.MapGrid[61]);
			Assert.AreEqual(1, level2.MapGrid[0]);
			Assert.AreEqual(0, level2.MapGrid[481]);
			Assert.AreEqual(1, level2.MapGrid[480]);
			Assert.AreEqual(720, level2.MapGrid.Length);
		}

		[Test]
		public void LoadAllLevels()
		{
			// Not a standard unit test - just tries to make sure that every valid level id actually loads a level.

			foreach (LevelId levelId in (LevelId[])Enum.GetValues(typeof(LevelId)))
			{
				var level = new Level(levelId);
			}
		}
	}
}

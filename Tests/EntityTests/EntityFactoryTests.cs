using System;
using RLEngine.Entities;
using RLEngine.Entities.MapTiles;
using NUnit.Framework;

namespace RLEngine.Tests
{
	[TestFixture]
	public class TestEntityFactory
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
		public void TestGetMapTile()
		{
			//TODO: Add full set of tests here.

			// Test error message and default handling.
			ErrorLogger.ClearTestMessages();
			var tile3 = EntityFactory.CreateMapTile(TileType.TestTile3);
			Assert.AreEqual("Unknown map tile type: TestTile3", ErrorLogger.GetNextTestMessage());
			Assert.IsNull(tile3);
		}
	}
}

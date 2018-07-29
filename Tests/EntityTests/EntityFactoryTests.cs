// Tidied up for version 0.3.

using RLEngine.Entities;
using RLEngine.Entities.MapTiles;
using NUnit.Framework;
using System.Collections.Generic;

namespace RLEngine.Tests
{
	[TestFixture]
	public class EntityFactoryTests
	{
		static string databasePath = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "UnitTestFiles");

		[SetUp]
		public void Setup()
		{
			StaticDatabase.StaticDatabaseConnection.SetToTest(databasePath);
			StaticDatabase.StaticDatabaseConnection.OpenDBConnection();
		}

		[TearDown]
		public void TearDown()
		{
			StaticDatabase.StaticDatabaseConnection.CloseDBConnection();
		}

		[Test]
		public void TestCreateMapTile()
		{
			var mapTile1 = EntityFactory.CreateMapTile(TileType.TestTile1);

			Assert.AreEqual("GraySeven", mapTile1.BackgroundColor);
			Assert.AreEqual("GrayFour", mapTile1.FogColor);
			Assert.IsTrue(mapTile1.HasTrait(Trait.TestTrait2));
			Assert.IsFalse(mapTile1.HasTrait(Trait.TestTrait1));

			var mapTile2 = EntityFactory.CreateMapTile("TestTile2");

			Assert.AreEqual("LightBlue", mapTile2.BackgroundColor);
			Assert.AreEqual("Blue", mapTile2.FogColor);
			Assert.IsTrue(mapTile2.HasTrait(Trait.BlockMove));
			Assert.IsFalse(mapTile2.HasTrait(Trait.Immobilised));
		}

		[Test]
		public void TestCreateFurnishing()
		{
			var furnishing1 = EntityFactory.CreateFurnishing("TestFurnishing1", 3, 5, new Dictionary<string, string>());

			Assert.AreEqual("Red", furnishing1.FGColorName);
			Assert.IsFalse(furnishing1.Concealed);
			Assert.IsTrue(furnishing1.HasTrait(Trait.BlockMove));
			Assert.AreEqual(3, furnishing1.XLoc);
			Assert.AreEqual(5, furnishing1.YLoc);

			var furnishing2 = EntityFactory.CreateFurnishing("TestFurnishing2", 6, 9, new Dictionary<string, string>());

			Assert.AreEqual("Olive", furnishing2.FGColorName);
			Assert.AreEqual('*', furnishing2.Symbol);
			Assert.IsTrue(furnishing2.HasTrait(Trait.TestTrait1));
			Assert.IsFalse(furnishing2.HasTrait(Trait.BlockMove));
			Assert.AreEqual(6, furnishing2.XLoc);
			Assert.AreEqual(9, furnishing2.YLoc);
		}
	}
}

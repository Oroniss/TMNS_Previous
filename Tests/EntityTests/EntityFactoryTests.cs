using TMNS.Entities;
using TMNS.Entities.MapTiles;
using NUnit.Framework;
using System.Collections.Generic;

namespace TMNS.Tests
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
			var mapTile1 = EntityFactory.CreateMapTile(TileType.StoneFloor);

			Assert.AreEqual("Silver", mapTile1.BackgroundColor);
			Assert.AreEqual("GraySix", mapTile1.FogColor);
			Assert.IsFalse(mapTile1.HasTrait(Trait.BlockLOS));
			Assert.IsFalse(mapTile1.HasTrait(Trait.BlockMove));

			var mapTile2 = EntityFactory.CreateMapTile("Ice");

			Assert.AreEqual("LightSkyBlue", mapTile2.BackgroundColor);
			Assert.AreEqual("SteelBlueThree", mapTile2.FogColor);
			Assert.AreEqual("a patch of ice", mapTile2.Description);
			Assert.IsFalse(mapTile2.HasTrait(Trait.BlockMove));
			Assert.IsFalse(mapTile2.HasTrait(Trait.BlockLOS));
		}

		[Test]
		public void TestCreateFurnishing()
		{
			var furnishing1 = EntityFactory.CreateFurnishing("Web", 3, 5, new Dictionary<string, string>());

			Assert.AreEqual("White", furnishing1.FGColorName);
			Assert.IsFalse(furnishing1.Concealed);
			Assert.IsFalse(furnishing1.HasTrait(Trait.BlockMove));
			Assert.AreEqual(3, furnishing1.XLoc);
			Assert.AreEqual(5, furnishing1.YLoc);

			var furnishing2 = EntityFactory.CreateFurnishing("Statue", 6, 9, new Dictionary<string, string>());

			Assert.AreEqual("GrayTwo", furnishing2.FGColorName);
			Assert.AreEqual('#', furnishing2.Symbol);
			Assert.IsTrue(furnishing2.HasTrait(Trait.BlockMove));
			Assert.IsFalse(furnishing2.HasTrait(Trait.Beetle));
			Assert.AreEqual(6, furnishing2.XLoc);
			Assert.AreEqual(9, furnishing2.YLoc);
		}
	}
}

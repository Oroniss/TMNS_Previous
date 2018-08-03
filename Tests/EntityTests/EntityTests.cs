using NUnit.Framework;
using TMNS.Entities;
using System.Collections.Generic;

namespace TMNS.Tests
{
	[TestFixture]
	public class EntityTests
	{
		static string defaultDebugMessage = "No Debug Messages";
		static string databasePath = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "UnitTestFiles");

		[SetUp]
		public void Setup()
		{
			ErrorLogger.SetToTest();
			ErrorLogger.ClearTestMessages();
			StaticDatabase.StaticDatabaseConnection.SetToTest(databasePath);
			StaticDatabase.StaticDatabaseConnection.OpenDBConnection();
		}

		[TearDown]
		public void TearDown()
		{
			StaticDatabase.StaticDatabaseConnection.CloseDBConnection();
		}

		[Test]
		public void TestEntityDetails()
		{
			var details1 = new EntityBasicDetails("Entity1", '^', "Blue", new List<Trait> {Trait.Ape, Trait.Immobilised });

			Assert.AreEqual('^', details1.Symbol);
			Assert.AreEqual("Blue", details1.FGColorName);
			Assert.IsTrue(details1.Traits.Contains(Trait.Ape));
			Assert.IsTrue(details1.Traits.Contains(Trait.Immobilised));
			Assert.IsFalse(details1.Traits.Contains(Trait.Angel));

			var details2 = new EntityBasicDetails("Entity2", ' ', "Green", new List<Trait> { Trait.Incorporeal, Trait.BlockMove });

			Assert.AreEqual(' ', details2.Symbol);
			Assert.AreEqual("Green", details2.FGColorName);
			Assert.IsTrue(details2.Traits.Contains(Trait.Incorporeal));
			Assert.IsTrue(details2.Traits.Contains(Trait.BlockMove));
			Assert.IsFalse(details2.Traits.Contains(Trait.Immobilised));
		}

		[Test]
		public void TestEntityBasicAttributes()
		{
			var entity1 = EntityFactory.CreateFurnishing("Barricade", 8, 12, new Dictionary<string, string>());

			Assert.AreEqual('|', entity1.Symbol);
			Assert.AreEqual("BurntSienna", entity1.FGColorName);
			Assert.AreEqual("Barricade", entity1.EntityName);
			Assert.AreEqual("a barricade", entity1.GetDescription());
			Assert.AreEqual("Barricade", entity1.ToString());
			Assert.AreEqual(8, entity1.XLoc);
			Assert.AreEqual(12, entity1.YLoc);
			Assert.IsFalse(entity1.Concealed);
			Assert.IsTrue(entity1.PlayerSpotted);

			var entity2 = EntityFactory.CreateFurnishing("Well", 15, 5, new Dictionary<string, string>());

			Assert.AreEqual('&', entity2.Symbol);
			Assert.AreEqual("BlueTwo", entity2.FGColorName);
			Assert.AreEqual("Well", entity2.EntityName);
			Assert.AreEqual("a well", entity2.GetDescription());
			Assert.AreEqual("Well", entity2.ToString());
			Assert.AreEqual(15, entity2.XLoc);
			Assert.AreEqual(5, entity2.YLoc);
			Assert.IsFalse(entity2.Concealed);
			Assert.IsTrue(entity2.PlayerSpotted);
		}

		[Test]
		public void TestEntityTraits()
		{
			var entity1 = EntityFactory.CreateFurnishing("Barricade", 8, 12, new Dictionary<string, string>());

			Assert.IsTrue(entity1.HasTrait(Trait.BlockMove));
			Assert.IsFalse(entity1.HasTrait(Trait.BlockLOS));
			Assert.IsFalse(entity1.HasTrait(Trait.Immobilised));

			entity1.AddTrait(Trait.Immobilised);

			Assert.IsTrue(entity1.HasTrait(Trait.Immobilised));

			entity1.RemoveTrait(Trait.BlockMove);

			Assert.IsFalse(entity1.HasTrait(Trait.BlockMove));


			var entity2 = EntityFactory.CreateFurnishing("Chest", 15, 5, new Dictionary<string, string>());

			Assert.IsFalse(entity2.HasTrait(Trait.Blindsight));
			Assert.IsTrue(entity2.HasTrait(Trait.BlockMove));

			entity2.AddTrait(Trait.Blindsight);

			Assert.IsTrue(entity2.HasTrait(Trait.Blindsight));

			entity2.RemoveTrait(Trait.Blindsight);

			Assert.IsFalse(entity2.HasTrait(Trait.Blindsight));

			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			entity2.RemoveTrait(Trait.Blindsight);

			Assert.AreEqual("Tried to remove non-existant trait from entity. Entity: Chest, Trait: TestTrait1",
			                ErrorLogger.GetNextTestMessage());
		}

		[Test]
		public void TestEntityOtherAttributes()
		{
			var entity1 = EntityFactory.CreateFurnishing("Barricade", 8, 12, new Dictionary<string, string>());

			Assert.IsFalse(entity1.HasOtherAttribute("TrapType"));

			entity1.SetOtherAttribute("TrapType", "FlameVent");

			Assert.IsTrue(entity1.HasOtherAttribute("TrapType"));
			Assert.AreEqual("FlameVent", entity1.GetOtherAttributeValue("TrapType"));

			entity1.SetOtherAttribute("TrapType", null);

			Assert.IsFalse(entity1.HasOtherAttribute("TrapType"));

			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			Assert.AreEqual("", entity1.GetOtherAttributeValue("TrapType"));

			Assert.AreEqual("Tried to get unknown attribute value TrapType on entity Barricade",
							ErrorLogger.GetNextTestMessage());
		}

		[Test]
		public void TestEntityUpdate()
		{
			// TODO: Add in once effects are in.
		}

		[Test]
		public void TestEntityDestruction()
		{
			// TODO: Add once properly in game.
		}

		[Test]
		public void TestEntitySetupFunctions()
		{
			ErrorLogger.ClearTestMessages();

			var entity1 = EntityFactory.CreateFurnishing("TestFurnishing1", 8, 12, new Dictionary<string, string>
			{{"Concealed", "True"} });

			Assert.AreEqual("Incorrect Concealment Specification at: 8, 12", ErrorLogger.GetNextTestMessage());
		}

		[Test]
		public void TestConcealedEntity()
		{
			var entity1 = EntityFactory.CreateFurnishing("TestFurnishing1", 8, 12, new Dictionary<string, string>
			{{"Concealed", "True"}, {"SpotDC", "3"} });

			Assert.IsFalse(entity1.PlayerSpotted);
			Assert.IsTrue(entity1.Concealed);
			Assert.AreEqual(' ', entity1.Symbol);
			Assert.AreEqual(3, entity1.SpotDC);
		}
	}
}

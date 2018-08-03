using System.Collections.Generic;
using NUnit.Framework;
using TMNS.Entities;
using TMNS.Entities.Furnishings;

namespace TMNS
{
	[TestFixture]
	public class FurnishingTests
	{
		static string defaultDebugMessage = "No Debug Messages";
		static string databasePath = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "UnitTestFiles");

		[SetUp]
		public void Setup()
		{
			ErrorLogger.SetToTest();
			Furnishing.ClearFurnishingIds();
			StaticDatabase.StaticDatabaseConnection.SetToTest(databasePath);
			StaticDatabase.StaticDatabaseConnection.OpenDBConnection();
		}

		[TearDown]
		public void TearDown()
		{
			StaticDatabase.StaticDatabaseConnection.CloseDBConnection();	
		}

		[Test]
		public void TestFurnishingBasics()
		{
			var furnishing1 = EntityFactory.CreateFurnishing("Barricade", 2, 1, new Dictionary<string, string>());

			Assert.AreEqual('|', furnishing1.Symbol);
			Assert.IsNull(furnishing1.InteractionFunctionName);

			var furnishing2 = EntityFactory.CreateFurnishing("Closet", 3, 5, new Dictionary<string, string>());

			Assert.AreEqual('#', furnishing2.Symbol);
			Assert.IsNull(furnishing2.InteractionFunctionName);
		}

		[Test]
		public void TestFurnishingDetails()
		{
			// TODO: Add this test once there is something on the furnishing details.
		}

		[Test]
		public void TestFurnishingSetupFunctions()
		{
			var paramDict1 = new Dictionary<string, string>
			{
				{"LevelTransition", "True"},
				{"DestinationLevel", "TestLevel2"},
				{"DestinationXLoc", "5"},
				{"DestinationYLoc", "12"}
			};

			var furnishing1 = EntityFactory.CreateFurnishing("Stair", 2, 3, paramDict1);

			Assert.IsTrue(furnishing1.HasOtherAttribute("DestinationLevel"));
			Assert.IsTrue(furnishing1.HasOtherAttribute("DestinationXLoc"));
			Assert.IsTrue(furnishing1.HasOtherAttribute("DestinationYLoc"));
			Assert.AreEqual("TestLevel2", furnishing1.GetOtherAttributeValue("DestinationLevel"));
			Assert.AreEqual("5", furnishing1.GetOtherAttributeValue("DestinationXLoc"));
			Assert.AreEqual("12", furnishing1.GetOtherAttributeValue("DestinationYLoc"));

			var paramDict2 = new Dictionary<string, string>
			{
				{"LevelTransition", "True"},
				{"DestinationXLoc", "5"},
				{"DestinationYLoc", "12"}
			};

			ErrorLogger.ClearTestMessages();

			var furnishing2 = EntityFactory.CreateFurnishing("Stair", 1, 2, paramDict2);

			Assert.IsFalse(furnishing2.HasOtherAttribute("DestinationXLoc"));
			Assert.IsFalse(furnishing2.HasOtherAttribute("DestinationYLoc"));
			Assert.AreEqual("Incorrect Level Transition Object Specification at: 1, 2", ErrorLogger.GetNextTestMessage());
			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());


			var paramDict3 = new Dictionary<string, string>
			{
				{"InteractionTrap", "True"},
				{"TrapType", "PoisonDart"},
				{"TrapLevel", "3"}
			};

			var furnishing3 = EntityFactory.CreateFurnishing("Chest", 3, 4, paramDict3);

			Assert.AreEqual("PoisonDartInteraction", furnishing3.InteractionTrapName);
			Assert.IsTrue(furnishing3.HasOtherAttribute("TrapType"));
			Assert.IsTrue(furnishing3.HasOtherAttribute("TrapLevel"));
			Assert.AreEqual("PoisonDart", furnishing3.GetOtherAttributeValue("TrapType"));
			Assert.AreEqual("3", furnishing3.GetOtherAttributeValue("TrapLevel"));

			var paramDict4 = new Dictionary<string, string>
			{
				{"InteractionTrap", "True"},
				{"TrapLevel", "5"}
			};

			ErrorLogger.ClearTestMessages();

			var furishing4 = EntityFactory.CreateFurnishing("Chest", 1, 2, paramDict4);

			Assert.IsNull(furishing4.InteractionTrapName);
			Assert.IsFalse(furishing4.HasOtherAttribute("TrapType"));
			Assert.IsFalse(furishing4.HasOtherAttribute("TrapLevel"));
			Assert.AreEqual("Incorrect Interaction Trap Specification at: 1, 2", ErrorLogger.GetNextTestMessage());
			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());
		}

		[Test]
		public void TestInteractionFunctions()
		{
			// TODO: Add in once actors/damage is in place.
		}

		[Test]
		public void TestMovementFunctions()
		{
			// TODO: Add these in when movement functions go in.
		}

		[Test]
		public void SaveAndLoadFurnishings()
		{
			var furnishing1 = EntityFactory.CreateFurnishing("TestFurnishing1", 3, 4, new Dictionary<string, string>());
			var furnishing2 = EntityFactory.CreateFurnishing("TestFurnishing2", 5, 6, new Dictionary<string, string>());

			var paramDict3 = new Dictionary<string, string>
			{
				{"InteractionTrap", "True"},
				{"TrapType", "PoisonDart"},
				{"TrapLevel", "3"}
			};

			var furnishing3 = EntityFactory.CreateFurnishing("TestFurnishing1", 3, 4, paramDict3);

			Assert.AreEqual(1, furnishing1.FurnishingId);
			Assert.AreEqual(2, furnishing2.FurnishingId);
			Assert.AreEqual(3, furnishing3.FurnishingId);

			var saveData = Furnishing.GetSaveData();
			Furnishing.LoadSaveData(saveData);

			var loadedFurnishing1 = Furnishing.GetFurnishing(1);
			var loadedFurnishing2 = Furnishing.GetFurnishing(2);
			var loadedFurnishing3 = Furnishing.GetFurnishing(3);

			Assert.AreEqual(furnishing1, loadedFurnishing1);
			Assert.AreEqual(furnishing2, loadedFurnishing2);
			Assert.AreEqual(furnishing3, loadedFurnishing3);
		}

		[Test]
		public void TestFurnishingIds()
		{
			var furnishing1 = EntityFactory.CreateFurnishing("TestFurnishing1", 3, 4, new Dictionary<string, string>());
			var furnishing2 = EntityFactory.CreateFurnishing("TestFurnishing2", 5, 6, new Dictionary<string, string>());

			var paramDict3 = new Dictionary<string, string>
			{
				{"InteractionTrap", "True"},
				{"TrapType", "PoisonDart"},
				{"TrapLevel", "3"}
			};

			var furnishing3 = EntityFactory.CreateFurnishing("TestFurnishing1", 3, 4, paramDict3);

			Assert.AreEqual(1, furnishing1.FurnishingId);
			Assert.AreEqual(2, furnishing2.FurnishingId);
			Assert.AreEqual(3, furnishing3.FurnishingId);

			Assert.AreEqual(furnishing1, Furnishing.GetFurnishing(1));
			Assert.AreEqual(furnishing2, Furnishing.GetFurnishing(2));
			Assert.AreEqual(furnishing3, Furnishing.GetFurnishing(3));

			furnishing2.Dispose();

			ErrorLogger.ClearTestMessages();

			Assert.IsNull(Furnishing.GetFurnishing(2));
			Assert.AreEqual("Tried to get unknown furnishing id: 2", ErrorLogger.GetNextTestMessage());

			furnishing1.Dispose();

			ErrorLogger.ClearTestMessages();

			Assert.IsNull(Furnishing.GetFurnishing(1));
			Assert.AreEqual("Tried to get unknown furnishing id: 1", ErrorLogger.GetNextTestMessage());

			var furnishing4 = EntityFactory.CreateFurnishing("TestFurnishing1", 3, 5, new Dictionary<string, string>());

			Assert.AreEqual(2, furnishing4.FurnishingId);

			var furnishing5 = EntityFactory.CreateFurnishing("TestFurnishing2", 1, 2, new Dictionary<string, string>());

			Assert.AreEqual(1, furnishing5.FurnishingId);
		}
	}
}

﻿using System.Collections.Generic;
using NUnit.Framework;
using RLEngine.Entities;
using RLEngine.Entities.Furnishings;

namespace RLEngine
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
			var furnishing1 = EntityFactory.CreateFurnishing("TestFurnishing1", 2, 1, new Dictionary<string, string>());

			Assert.AreEqual('#', furnishing1.Symbol);
			Assert.AreEqual("TestInteractionFunction1", furnishing1.InteractionFunctionName);

			var furnishing2 = EntityFactory.CreateFurnishing("TestFurnishing2", 3, 5, new Dictionary<string, string>());

			Assert.AreEqual('*', furnishing2.Symbol);
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

			var furnishing1 = EntityFactory.CreateFurnishing("TestFurnishing2", 2, 3, paramDict1);

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

			var furnishing2 = EntityFactory.CreateFurnishing("TestFurnishing2", 1, 2, paramDict2);

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

			var furnishing3 = EntityFactory.CreateFurnishing("TestFurnishing1", 3, 4, paramDict3);

			Assert.AreEqual("PoisonDartInteraction", furnishing3.InteractionTrapName);
			Assert.IsTrue(furnishing3.HasOtherAttribute("TrapType"));
			Assert.IsTrue(furnishing3.HasOtherAttribute("TrapLevel"));
			Assert.AreEqual("PoisonDart", furnishing3.GetOtherAttributeValue("TrapType"));
			Assert.AreEqual("3", furnishing3.GetOtherAttributeValue("TrapLevel"));

			Assert.AreEqual("TestInteractionFunction1", furnishing3.InteractionFunctionName);

			var paramDict4 = new Dictionary<string, string>
			{
				{"InteractionTrap", "True"},
				{"TrapLevel", "5"}
			};

			ErrorLogger.ClearTestMessages();

			var furishing4 = EntityFactory.CreateFurnishing("TestFurnishing1", 1, 2, paramDict4);

			Assert.IsNull(furishing4.InteractionTrapName);
			Assert.IsFalse(furishing4.HasOtherAttribute("TrapType"));
			Assert.IsFalse(furishing4.HasOtherAttribute("TrapLevel"));
			Assert.AreEqual("Incorrect Interaction Trap Specification at: 1, 2", ErrorLogger.GetNextTestMessage());
			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			Assert.AreEqual("TestInteractionFunction1", furishing4.InteractionFunctionName);
		}

		[Test]
		public void TestInteractionFunctions()
		{
			// TODO: Add this in
		}

		[Test]
		public void TestMovementFunctions()
		{
			// TODO: Add these in when movement functions go in.
		}

		[Test]
		public void SaveAndLoadFurnishings()
		{
			// TODO: Add this in
		}

		[Test]
		public void TestFurnishingIds()
		{
			// TODO: Add this in.
		}
	}
}

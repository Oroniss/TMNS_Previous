// Finished up for version 0.1.

using NUnit.Framework;
using RLEngine.UserData;

namespace RLEngine.Tests
{
	[TestFixture]
	public class SaveDataTests
	{
		[Test]
		public void TestGameData()
		{
			var data1 = new GameData();
			Assert.AreEqual(0, data1.GameID);
			Assert.IsNull(data1.GameIdentifier);

			var data2 = new GameData(101, "Fred");
			Assert.AreEqual(101, data2.GameID);
			Assert.AreEqual("Fred", data2.GameIdentifier);
		}

		[Test]
		public void TestSaveGameSummary()
		{
			var data1 = new GameData(99, "Aleasha");
			var summary1 = new SaveGameSummary(data1, "Large Test Level", true);

			Assert.AreEqual(99, summary1.GameData.GameID);
			Assert.AreEqual("Aleasha", summary1.GameData.GameIdentifier);
			Assert.AreEqual("Large Test Level", summary1.CurrentLevelName);
			Assert.IsTrue(summary1.StillAlive);
			Assert.AreEqual("Game Identifier: Aleasha, Current Level: Large Test Level, Still Alive: True", 
			                summary1.ToString());
		}

		[Test]
		public void TestSaveGame()
		{
			var data1 = new GameData(99, "Aleasha");
			var summary1 = new SaveGameSummary(data1, "Large Test Level", false);

			var saveGame = new SaveGame(summary1);

			Assert.AreEqual(99, summary1.GameData.GameID);
			Assert.IsFalse(summary1.StillAlive);
		}
	}
}

using System.Collections.Generic;
using System.IO;
using System;
using RLEngine.Entities.MapTiles;

namespace RLEngine.Levels.LevelDatabase
{
	public static class LevelDatabase
	{
		static string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Levels", "LevelFiles");
		static Dictionary<LevelId, string> levelFilePaths = new Dictionary<LevelId, string>()
		{
			{LevelId.TestLevel1, Path.Combine("TestLevels", "TestLevel1.txt")},
			{LevelId.TestLevel2, Path.Combine("TestLevels", "TestLevel2.txt")}
		};

		public static LevelDetails GetLevelTemplate(LevelId levelId)
		{
			var levelFilePath = Path.Combine(filePath, levelFilePaths[levelId]);

			var fileReader = new StreamReader(levelFilePath);

			// Get level name first
			var levelName = fileReader.ReadLine().Trim();

			LevelDetails levelTemplate = new LevelDetails(levelName, levelId);

			if ((LevelId)Enum.Parse(typeof(LevelId), fileReader.ReadLine().Trim()) != levelId)
			{
				// TODO: Fix this up.
				ErrorLogger.AddDebugText("");
				return levelTemplate;
			}

			levelTemplate.MapWidth = int.Parse(fileReader.ReadLine());
			levelTemplate.MapHeight = int.Parse(fileReader.ReadLine());

			if (fileReader.ReadLine().Trim() != "###")
			{
				// TODO: Fix this too.
				return levelTemplate;
			}

			// Tile Dictionary
			string line;
			while ((line = fileReader.ReadLine().Trim()) != "###")
			{
				var splitLine = line.Split(',');
				levelTemplate.TileDictionary[int.Parse(splitLine[0])] = (TileType)Enum.Parse(typeof(TileType), splitLine[1]);
			}

			// TileMap
			levelTemplate.MapGrid = new int[levelTemplate.MapHeight * levelTemplate.MapWidth];
			for (int y = 0; y < levelTemplate.MapHeight; y++)
			{
				var splitLine = fileReader.ReadLine().Trim().Split(',');
				for (int x = 0; x < levelTemplate.MapWidth; x++)
					levelTemplate.MapGrid[y * levelTemplate.MapWidth + x] = int.Parse(splitLine[x]);
			}

			if (fileReader.ReadLine().Trim() != "###")
			{
				// TODO: Fix this
				return levelTemplate;
			}


			fileReader.Close();

			return levelTemplate;
		}

		public static void SetTestFilePath(string testContext)
		{
			filePath = Path.Combine(testContext, "Levels", "LevelFiles");
		}
	}
}

using System.Collections.Generic;
using System.IO;
using System;
using TMNS.Entities.MapTiles;

namespace TMNS.Levels.LevelDatabase
{
	public static class LevelDatabase
	{
		static string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Levels", "LevelFiles");
		static Dictionary<LevelId, string> levelFilePaths = new Dictionary<LevelId, string>
		{
			{LevelId.Level1A, "Level1A.txt"},
			{LevelId.Level2A, "Level2A.txt"},
			{LevelId.TestLevel1, Path.Combine("TestLevels", "TestLevel1.txt")},
			{LevelId.TestLevel2, Path.Combine("TestLevels", "TestLevel2.txt")}
		};

		public static LevelDetails GetLevelTemplate(LevelId levelId)
		{
			// TODO: Don't spend too much time here - will get put into a DB in the end anyway.

			var levelFilePath = Path.Combine(filePath, levelFilePaths[levelId]);

			var fileReader = new StreamReader(levelFilePath);

			// Get level name first
			var levelName = fileReader.ReadLine().Trim();

			var levelTemplate = new LevelDetails(levelName, levelId);

			if ((LevelId)Enum.Parse(typeof(LevelId), fileReader.ReadLine().Trim()) != levelId)
			{
				ErrorLogger.AddDebugText(string.Format("Level template is broken - level id doesn't match for level {0}", levelId));
				fileReader.Close();
				return levelTemplate;
			}

			levelTemplate.MapWidth = int.Parse(fileReader.ReadLine());
			levelTemplate.MapHeight = int.Parse(fileReader.ReadLine());

			if (fileReader.ReadLine().Trim() != "###")
			{
				ErrorLogger.AddDebugText(string.Format("Header section for level {0}, has problems", levelId));
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
				ErrorLogger.AddDebugText(string.Format("Map section for level {0} has problems", levelId));
				return levelTemplate;
			}

			// Furnishings
			while ((line = fileReader.ReadLine().Trim()) != "###")
			{
				var splitLine = line.Split(',');
				levelTemplate.Furnishings.Add(splitLine);
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

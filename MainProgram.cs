// Finished up for version 0.1.

using System;
using System.Threading;
using RLEngine.UserInterface;
using RLNET;

namespace RLEngine
{
	public static class MainProgram
	{
		const string EngineVersion = "Version 0.1";

		// TODO: Put these into a config file somewhere.
		static readonly string _fontName = "terminal8x8.png";
		static readonly int _consoleWidth = 160;
		static readonly int _consoleHeight = 80;
		static readonly int _fontSize = 8;
		static readonly float _scale = 1.0f;
		static readonly string _windowTitle = "Halfbreed";

		static RLRootConsole rootConsole;

		// Global attributes
		static Levels.Level _currentLevel;
		static int _currentTime;
		static bool _quit = false;

		public static void Main()
		{
			UserDataManager.SetupDirectoriesAndFiles();

			var configParameters = UserDataManager.ReadConfigParameters();

			UserInputHandler.ExtraKeys = configParameters.ExtraKeys;
			UserDataManager.FullLogging = configParameters.FullLogging;
			// TODO: Set the gm option here too.

			rootConsole = new RLRootConsole(_fontName, _consoleWidth, _consoleHeight, _fontSize, _fontSize, _scale,
											_windowTitle);

			rootConsole.Update += RootConsoleUpdate;
            rootConsole.Render += RootConsoleRender;

			var mainLoopThread = new Thread(RunStartMenu);
			mainLoopThread.Start();
            rootConsole.Run();

		}

		static void RootConsoleRender(object sender, EventArgs e)
		{
			if (MainGraphicDisplay.IsDirty)
			{
				rootConsole.Clear();
				rootConsole = MainGraphicDisplay.CopyDisplayToRootConsole(rootConsole);
				rootConsole.Draw();
			}
		}

		static void RootConsoleUpdate(object sender, EventArgs e)
		{
			var key = rootConsole.Keyboard.GetKeyPress();
			if (key != null)
			{
				UserInputHandler.AddKeyboardInput(key.Key);
			}
		}

		static void RunStartMenu()
		{
			var gameId = MenuProvider.MainMenu.DisplayMainMenu();
			if (gameId == -1)
			{
				Quit();
				return;
			}

			_currentLevel = new Levels.Level(Levels.LevelId.TestLevel2);

			/*
			var gameState = UserDataManager.GetGameState(gameId);
			if (gameState.Summary.CurrentLevelName == "NEWGAME")
			{
				SetupNewGame(gameState.Summary.GameData);
				LevelTransition(_startingLevel, _startingXLoc, _startingYLoc);
			}
			else
				LoadGame(gameState);
			*/
			RunGame();
			Quit();
		}

		static void RunGame()
		{
			while (true)
			{
				MainGraphicDisplay.UpdateGameScreen();

				var key = UserInputHandler.GetNextKey();
				if (key == "ESCAPE")
					_quit = true;

				if (_quit)
				{
					//SaveGame();
					return;
				}
				_currentTime++;
			}
		}

		public static void Quit()
		{
			_quit = true;
			rootConsole.Close();
		}
		public static int CurrentTime
		{
			get { return _currentTime; }
		}

		public static Levels.Level CurrentLevel
		{
			get { return _currentLevel; }
		}

		public static int PlayerXLoc
		{
			get { return 0; }
		}

		public static int PlayerYLoc
		{
			get { return 0; }
		}
	}
}

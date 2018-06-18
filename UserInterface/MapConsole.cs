using RLNET;
using RLEngine.Resources.Palette;

namespace RLEngine.UserInterface
{
	public class MapConsole:BaseConsole
	{
		public MapConsole(int width, int height, int left, int top, RLColor backColor, BackConsole backConsole)
			:base(width, height, left, top, backColor, backConsole)
		{
		}

		public void DrawMap()
		{
			DrawMap(MainProgram.CurrentLevel, MainProgram.Player.XLoc, MainProgram.Player.YLoc);
		}

		public void DrawMap(Levels.Level level, int xCentre, int yCentre) // TODO: Add hashset of visible tiles.
		{
			Clear();

			var xLimits = getDrawingLimits(level.MapWidth, _console.Width, xCentre);
			var yLimits = getDrawingLimits(level.MapHeight, _console.Height, yCentre);

			for (int y = yLimits.Min; y < yLimits.Max; y++)
			{
				for (int x = xLimits.Min; x < xLimits.Max; x++)
				{
					// TODO: Fix this later.
					if (true) //level.IsRevealed(x, y))
					{
						//_console.Set(x + xLimits.Offset, y + yLimits.Offset, null,
						//			 Palette.GetColor(level.GetFogColor(x, y)), ' ');

						// TODO: Temporary code here.
						if (level.HasDrawingEntity(x, y))
						{
							var entity = level.GetDrawingEntity(x, y);
							_console.Set(x + xLimits.Offset, y + yLimits.Offset, Palette.GetColor(entity.FGColorName),
										 Palette.GetColor(level.GetBGColor(x, y)), entity.Symbol);
						}
						else
						{
							_console.Set(x + xLimits.Offset, y + yLimits.Offset, null, Palette.GetColor(level.GetBGColor(x, y)), ' ');
						}
					}
				}
			}

			/*
			foreach (var position in MainProgram.CurrentLevel.VisibleTiles)
			{
				_console.Set(position.X + xLimits.Offset, position.Y + yLimits.Offset, null,
							 Palette.GetColor(level.GetBackgroundColor(position.X, position.Y)), ' ');

				if (level.HasDrawingEntity(position.X, position.Y))
				{
					var entity = level.GetDrawingEntity(position.X, position.Y);
					_console.Set(position.X + xLimits.Offset, position.Y + yLimits.Offset, Palette.GetColor(entity.FGColorName),
								 null, entity.Symbol);
				}
			}
			*/

			CopyToBackConsole();
		}

		MapDrawingLimits getDrawingLimits(int levelSize, int consoleSize, int drawingCentre)
		{
			int drawingSize = consoleSize - 4;
			int drawingMin = 0;
			int drawingMax = levelSize;
			int drawingOffset = 2;

			// We can fit the entire level
			if (levelSize <= drawingSize)
			{
				drawingOffset = (drawingSize - levelSize) / 2;
			}
			else
			{
				if (drawingCentre < drawingSize / 2)
				{
					drawingMin = drawingSize;
				}
				else
				{
					if (levelSize - drawingCentre < drawingSize / 2)
					{
						drawingMin = levelSize - drawingSize;
						drawingOffset -= drawingMin;
					}
					else
					{
						drawingMin = drawingCentre - drawingSize / 2;
						drawingOffset -= drawingMin;
						drawingMax = drawingCentre + (drawingSize / 2 - 1);
					}
				}
			}
			return new MapDrawingLimits(drawingMin, drawingMax, drawingOffset);
		}
	}

	struct MapDrawingLimits
	{
		public int Min;
		public int Max;
		public int Offset;

		public MapDrawingLimits(int min, int max, int offset)
		{
			Min = min;
			Max = max;
			Offset = offset;
		}
	}
}

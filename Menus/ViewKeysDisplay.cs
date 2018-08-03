using System.Collections.Generic;

namespace TMNS.Menus
{
	public class ViewKeysDisplay
	{
		readonly List<string> _keys = new List<string>
		{
			"A:  Acquire an item on the ground",
			"B:  Display the contents of your backpack",
			"C:  Display the character menu",
			"E:  Examine an object or creature",
			"H:  Setup or clear a hot-key slot",
			"I:  Use an item from your inventory",
			"J:  Display your quest journal",
			"K:  Display this list of commands",
			"P:  Kneel in Prayer or Pray at an altar",
			"S:  Search your surroundings",
			"T:  Talk to a creature or NPC",
			"U:  Use an object such as a door or lever",
			"V:  Display the achievement menu",
			"Y:  Use an ability or cast a spell",
			"",
			"Spacebar:  Pass your current turn",
			"Direction Keys:  Move or attack in the given direction"
		};

		// TODO: Add extra keys if present.

		public void ViewKeys()
		{
			MainGraphicDisplay.MenuConsole.DrawTextBlock("Available commands", _keys, "Press any key to return");
			UserInterface.UserInputHandler.GetNextKey();
		}
	}
}

using System.Collections.Generic;

namespace RLEngine.Menus
{
	public class ViewKeysDisplay
	{
		readonly List<string> _keys = new List<string>
		{
			"B:  Display the contents of your backpack",
			"C:  Display the character menu",
			"E:  Examine an object or creature",
			"H:  Setup or clear a hot-key slot",
			"I:  Use an item from your inventory",
			"J:  Display your quest journal",
			"K:  Display this list of commands",
			"S:  Search your surroundings",
			"T:  Talk to a creature or NPC",
			"U:  Use an object such as a door or lever",
			"V:  Display the achievement menu",
			"",
			"Spacebar:  Pass your current turn",
			"Direction Keys:  Move in the given direction"
		};

		public void ViewKeys()
		{
			MainGraphicDisplay.MenuConsole.DrawTextBlock("Available commands", _keys, "Press any key to return");
			UserInterface.UserInputHandler.GetNextKey();
		}
	}
}

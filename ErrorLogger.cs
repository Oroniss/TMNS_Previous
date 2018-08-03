using System.Collections.Generic;

namespace TMNS
{
	public static class ErrorLogger
	{
		static bool isTesting;
		static List<string> testMessages;

		public static void AddDebugText(string text)
		{
			AddDebugText(text, "White");
		}

		public static void AddDebugText(string text, string textColor)
		{
			if (isTesting)
			{
				testMessages.Add(text);
				System.Console.WriteLine(text);
			}

			// TODO: Put this in a log file somewhere.
		}

		public static void SetToTest()
		{
			isTesting = true;
			testMessages = new List<string>();
		}

		public static void ClearTestMessages()
		{
			if (isTesting)
				testMessages = new List<string>();
		}

		public static string GetNextTestMessage()
		{
			if (!isTesting || testMessages.Count == 0)
				return "No Debug Messages";
			var message = testMessages[0];
			testMessages.RemoveAt(0);
			return message;
		}
	}
}

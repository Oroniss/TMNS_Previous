using System;

namespace RLEngine.UserData
{
	[Serializable]
	public struct ConfigParameters
	{
		public bool ExtraKeys;
		public bool FullLogging;
		public bool GMOptions;

		public ConfigParameters(bool extraKeys, bool fullLogging, bool gmOptions)
		{
			ExtraKeys = extraKeys;
			FullLogging = fullLogging;
			GMOptions = gmOptions;
		}
	}
}

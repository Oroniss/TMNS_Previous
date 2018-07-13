using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace RLEngine.UserData
{
	public static class ApplicationSettings
	{
		static ISettings CurrentParameters
		{
			get { return CrossSettings.Current; }
		}

		public static bool ExtraKeys
		{
			get { return CurrentParameters.GetValueOrDefault("ExtraKeys", false); }
			set { CurrentParameters.AddOrUpdateValue("ExtraKeys", value); 
			}
		}

		public static bool FullLogging
		{
			get { return CurrentParameters.GetValueOrDefault("FullLogging", false); }
			set { CurrentParameters.AddOrUpdateValue("FullLogging", value); }
		}

		public static bool GMOptions
		{
			get { return CurrentParameters.GetValueOrDefault("GMOptions", false); }
			set { CurrentParameters.AddOrUpdateValue("GMOptions", value); }
		}
	}
}

// Finished up for version 0.1.

using NUnit.Framework;
using RLEngine.UserData;

namespace RLEngine.Tests
{
	[TestFixture]
	public class ConfigParametersTests
	{
		[Test]
		public void TestConfigParameters()
		{
			var params1 = new ConfigParameters(true, true, false);
			Assert.IsTrue(params1.ExtraKeys);
			Assert.IsTrue(params1.FullLogging);
			Assert.IsFalse(params1.GMOptions);
		}
	}
}

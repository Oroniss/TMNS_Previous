using RLNET;

namespace TMNS.UserInterface
{
	public class BackConsole:RLConsole
	{
		bool _isDirty;

		public BackConsole(int width, int height)
			:base(width, height)
		{
			_isDirty = true;
		}

		public bool IsDirty
		{
			get { return _isDirty; }
		}

		public void SetClean()
		{
			_isDirty = false;
		}

		public void SetDirty()
		{
			_isDirty = true;
		}
	}
}

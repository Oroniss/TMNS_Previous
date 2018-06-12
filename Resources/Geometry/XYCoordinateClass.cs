﻿namespace RLEngine.Resources.Geometry
{
	public class XYCoordinateClass
	{
		int _x;
		int _y;

		public XYCoordinateClass(int x, int y)
		{
			x = _x;
			y = _y;
		}

		public int X
		{
			get { return _x; }
			set { _x = value; }
		}

		public int Y
		{
			get { return _y; }
			set { _y = value; }
		}
	}
}
using System;

namespace GameBox
{
	namespace GBOD
	{
		public class GBODChar
		{
			private char ch;
			private ushort x;
			private uint y;

			internal GBODChar (ushort x_, uint y_, char ch_)
			{
				x = x_;
				y = y_;
				ch = ch_;
			}

			public char Ch
			{
				get { return ch; }
			}

			public ushort X
			{
				get { return x; }
			}

			public uint Y
			{
				get { return y; }
			}
		}
	}
}

